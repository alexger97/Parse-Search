using HtmlAgilityPack;
using ParseSearch.Interface;
using ParseSearch.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace ParseSearch.Service
{
    public class SearchService : ISearchService
    {
        static object block = new object();

        // static WebClient wc = new WebClient();



        #region MultySearch
        static WebClient wc1 = new WebClient();
        static WebClient wc2 = new WebClient();
        static WebClient wc3 = new WebClient();

        static bool wc1First;
        static bool wc2First;
        static bool wc3First;

        public static string Response2 { get; set; }
        public object[] SearchwithAll(string request)
        {
             
            wc1First = wc2First = wc3First = false;
            wc1.DownloadStringCompleted += Wc11_DownloadStringCompleted;
            wc2.DownloadStringCompleted += Wc22_DownloadStringCompleted;
            wc3.DownloadStringCompleted += Wc33_DownloadStringCompleted;

            wc1.DownloadStringAsync(new Uri("https://www.google.ru/search?q=") );
            wc2.DownloadStringAsync(new Uri("http://www.cyberforum.ru/csharp-beginners/thread1820108.html") );
            wc3.DownloadStringAsync(new Uri("http://www.cyberforum.ru/csharp-beginners/thread1820108.html") );



            Thread.Sleep(100000);


            if (wc1First) return new object[2] { ProcessingGoogleRezult(Response2), TypeOfSeacrhMachine.Google };
            if (wc2First) return new object[2] { ProcessingYaRezult(Response2), TypeOfSeacrhMachine.Yandex };
            if (wc3First) return new object[2] { ProcessingYahooRezult(Response2), TypeOfSeacrhMachine.Yahoo };

            return null;
        }

        private void Wc33_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Setblock(3, e.Result);
        }

        private void Wc22_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Setblock(2, e.Result);
        }

        private void Wc11_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Setblock(1, e.Result);
        }

        void Setblock(int i,string s)
        {
            lock (block)
            {
                if (wc1First == wc2First == wc3First== false)
                {
                    if (i == 1) wc1First = true;
                    if (i == 2) wc2First = true;
                    if (i == 3) wc3First = true;
                    Response2 = s;
                }
            }
        }
        

        #endregion



        #region GoogleSearch



        public List<SearchElementResult> SearchWithGoogle(string request)
        {
            var urlSearch = "https://www.google.ru/search?q=";
            string url = urlSearch + HttpUtility.UrlEncode(request) + "&ie=UTF-8&num=10";
            wc1.Encoding = Encoding.Default;
            string response = wc1.DownloadString(url);
            return ProcessingGoogleRezult(response);
        }


        public List<SearchElementResult> ProcessingGoogleRezult(string response)
        {
            List<SearchElementResult> SearchResultsLocal = null;
            try
            {

                HtmlDocument doc = new HtmlDocument();

                doc.LoadHtml(response);

                List<string> lsit = new List<string>();

                var main = doc.DocumentNode.SelectSingleNode("//div[@id=\"main\"]");

                var f1 = main.ChildNodes.Where(x => x.Name == "div" && !x.GetAttributeValue("class", "").Equals("ZINbbc xpd O9g5cc uUPGi")).ToList();

                var Results = f1.Where(x => x.ChildNodes.Count == 1 && x.ChildNodes[0].GetAttributeValue("class", "").Equals("ZINbbc xpd O9g5cc uUPGi") && x.ChildNodes[0].ChildNodes.Count == 3);

                int i = 0;
                SearchResultsLocal = new List<SearchElementResult>();
                foreach (var result in Results)
                {
                    var s0 = result.ChildNodes[0];

                    var smaldescription = s0.ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText;
                    var link = s0.ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText;
                    var backdescription = s0.ChildNodes[2].ChildNodes[0].InnerText;


                    SearchResultsLocal.Add(new SearchElementResult() { MainText = smaldescription, Description = backdescription, Link = link });

                    Debug.WriteLine("****");
                    Debug.WriteLine(++i);
                    Debug.WriteLine("Краткое описание :" + smaldescription);
                    Debug.WriteLine("Ссылка :" + link);
                    Debug.WriteLine("Полное описание :" + backdescription);

                }
            }
            catch (Exception x)
            { Debug.WriteLine("Ошибка при обработке запроса ." + x.Message + x.StackTrace); }
            return SearchResultsLocal;

        }
        #endregion

        #region YaSearch
        public List<SearchElementResult> YaSearch(string request)
        {
            var urlSearch = "https://yandex.ru/search/?text=";
            string url = urlSearch + HttpUtility.UrlEncode(request);
            wc2.Encoding = Encoding.UTF8;
            string response = wc2.DownloadString(url);
            
            return ProcessingYaRezult(response);
        }
        public List<SearchElementResult> ProcessingYaRezult(string response)
        {
            List<SearchElementResult> SearchElementResults = null;
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(response);

                var main = doc.DocumentNode.SelectSingleNode(".//ul[@class=\"serp-list serp-list_left_yes\"]");
                HtmlNode image;
                string path;
                if (main == null)
                {
                    string capth = "//div[@class=\"captcha__image\"]";
                    image = doc.DocumentNode.SelectSingleNode(capth);
                    path = image.GetAttributeValue("src", "true"); ;
                    /////....
                    return SearchElementResults;
                }
                SearchElementResults = new List<SearchElementResult>();

                var sucs = main.ChildNodes.Where(x => x.Name == "li" && x.GetAttributeValue("data-fast-wzrd", "@").Equals("@"));
                if (sucs != null) sucs = sucs.ToList();
                else
                    return SearchElementResults;

                foreach (var item in sucs)
                {
                    HtmlNode root;
                    if (item.ChildNodes[0].GetAttributeValue("class", "@").Contains("organic"))
                    {
                        if (!item.ChildNodes[0].ChildNodes.ToList().Exists(x => x.GetAttributeValue("class", "@").Contains("wrapper_thumb-position")))
                            root = item.ChildNodes[0];
                        else root = item.ChildNodes[0].ChildNodes.ToList().Find(x => x.GetAttributeValue("class", "@").Contains("wrapper_thumb-position")).ChildNodes[1];
                    }
                    else
                        root = item.ChildNodes[0].ChildNodes[0];

                    string descr;
                    var h2 = root.ChildNodes.ToList().Find(x => x.Name == "h2").InnerText;
                    var link = root.ChildNodes.ToList().Find(x => x.GetAttributeValue("class", "").Contains("organic__subtitle")).ChildNodes[0].InnerText;
                    var organicontainer = root.ChildNodes.ToList().Find(x => x.GetAttributeValue("class", "@").Contains("organic__content-wrapper"));
                    HtmlNode textcontainer;
                    if (!organicontainer.ChildNodes.ToList().Exists(x => x.GetAttributeValue("class", "@").Equals("wrapper")))
                        textcontainer = organicontainer.ChildNodes.ToList().Find(x => x.GetAttributeValue("class", "@").Contains("text-container"));
                    else
                    {
                        textcontainer = organicontainer.ChildNodes.ToList().Find(x => x.GetAttributeValue("class", "@").Contains("wrapper")).ChildNodes[1];
                    }
                    if (textcontainer.ChildNodes.ToList().Exists(x => x.GetAttributeValue("class", "@").Equals("extended - text__full")))
                        descr = textcontainer.ChildNodes.ToList().Find(x => x.GetAttributeValue("class", "@").Equals("extended - text__full")).InnerText;

                    descr = textcontainer.InnerText;

                    SearchElementResults.Add(new SearchElementResult() { Description = descr, Link = link, MainText = h2 });
                    Debug.WriteLine(h2);
                    Debug.WriteLine(link);
                    Debug.WriteLine(descr);

                }
            }
            catch (Exception x)
            {
                Debug.WriteLine("Ошибка при обработке запроса ." + x.Message + x.StackTrace);
                return SearchElementResults;
            }
            return SearchElementResults;
        }
        #endregion


        [Obsolete]
        public void BingSearch(string request)
        {
            wc1.Encoding = Encoding.UTF8;
            var urlSearch = "https://www.bing.com/search?q";
            string url = urlSearch + HttpUtility.UrlEncode("такси дешево спб ");

            string response = wc1.DownloadString(url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(response);
            List<string> lsit = new List<string>();
            //проблема с Bing в том, что не удается получить список резулатов при запросе. Через браузер работает, но здесь не удается получить
            var main = doc.DocumentNode.SelectSingleNode(".//ul[@class=\"serp-list serp-list_left_yes\"]");

        }


        #region YahooSearch


        public List<SearchElementResult> YahooSearch(string request)
        {
            wc3.Encoding = Encoding.UTF8;
            var urlSearch = "https://search.yahoo.com/search?p=";
            string url = urlSearch + HttpUtility.UrlEncode(request);
            string response = wc3.DownloadString(url);
     
            return ProcessingYahooRezult(response);
        }

        public List<SearchElementResult> ProcessingYahooRezult(string response)
        {
            List<SearchElementResult> Listresults = null;
            try
            {

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(response);

                var main2 = doc.DocumentNode.SelectSingleNode("//ol[@class=\"mb-15 reg searchCenterMiddle\"]"); //блок с ответами

                var roots = main2.ChildNodes.Where(x => x.ChildNodes[0].GetAttributeValue("class", "@").Contains("dd algo algo-sr")); //только стандартная компоновка : без картинок, песенок и т.д
                var workdirs = roots.Select(x => x.ChildNodes[0]);

                Listresults = new List<SearchElementResult>();
                foreach (var item in workdirs)
                {
                    var head = item.ChildNodes[0].ChildNodes[0].InnerText;
                    var link = item.ChildNodes[0].ChildNodes[2].InnerText;
                    var desc = item.ChildNodes.ToList().Find(x => x.GetAttributeValue("class", "@").Contains("compText")).InnerText;

                    Listresults.Add(new SearchElementResult() { Link = link, Description = desc, MainText = head });
                    Debug.WriteLine("***");
                    Debug.WriteLine(head);
                    Debug.WriteLine(link);
                    Debug.WriteLine(desc);
                    Debug.WriteLine("***");
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message + x.StackTrace + x.Source);
            }
            return Listresults;
        }


        #endregion




    }

}

