﻿using HtmlAgilityPack;
using ParseSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace ParseSearch.Service
{
  public  class SearchService
    {
        public SearchService()
        {
            SearchResults = new List<SearchResult>();
        }


        public List<SearchResult> SearchResults { get; set; }
        public void SearchGoogle()
        {


            System.Net.WebClient wc = new System.Net.WebClient();

            //wc.Encoding = Encoding.UTF8;
            var urlSearch = "https://www.google.ru/search?q=";
            string url = urlSearch + HttpUtility.UrlEncode("такси дешево спб ") + "&ie=UTF-8&num=10";
            string response = wc.DownloadString(url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(response);

            // var html = Encoding.GetEncoding(1252).GetString(wc.DownloadData(url));

            List<string> lsit = new List<string>();

            var main = doc.DocumentNode.SelectNodes("//div[@id=\"main\"]");
            var mw = doc.DocumentNode.SelectNodes("//div[@class=\"mw\"]");
            var mw2 = doc.DocumentNode.SelectNodes(".//div[@class=\"ZINbbc\"]");

            //foreach (var item in main[0].ChildNodes)
           //// {
             //   if (item.Name == "div")
                   // Console.WriteLine(item.GetAttributeValue("class", "").Equals("ZINbbc xpd O9g5cc uUPGi"));
           // }
            //   Console.WriteLine(item.GetAttributeValue("class",""));


            var yy = main[0].ChildNodes.Where(x => x.Name == "div" && !x.GetAttributeValue("class", "").Equals("ZINbbc xpd O9g5cc uUPGi"));//&&x.Attributes


            var Results = yy.ToList();
            // Results.RemoveRange(0, 3);

            var opp = Results.Where(x => x.ChildNodes.Count == 1 && x.ChildNodes[0].GetAttributeValue("class", "").Equals("ZINbbc xpd O9g5cc uUPGi") && x.ChildNodes[0].ChildNodes.Count == 3);
            //.Exists(z => z.Attributes[0].Value == "\"BUybKe\""));
            //  Results.RemoveRange(0, 2);

            //Results.RemoveRange(Results.Count - 3, 2);

            int i = 0;
            foreach (var result in opp)
            {

                var s0 = result.ChildNodes[0];

                var smaldescription = (string)s0.ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText;
                var link = s0.ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText;

                //var uy= s0.ChildNodes[2].SelectNodes(".//div[@class=\"BNeawe *\"]");
                var backdescription = s0.ChildNodes[2].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText;

                var ss1 = s0.ChildNodes[0].ChildNodes[0].Attributes["href"].Value;

                 
                SearchResults.Add(new SearchResult() { MainText = smaldescription, Description = backdescription, Link = link, TypeOfSeacrhMachine = TypeOfSeacrhMachine.Google });
                MessageBox.Show(smaldescription + backdescription + link);
                    
                    /*  Console.WriteLine("****");
                Console.WriteLine(++i);
                Console.WriteLine("Краткое описание :" + smaldescription);
                Console.WriteLine("Ссылка :" + link);
                Console.WriteLine("Полное описание :" + backdescription);
                Console.ReadLine();*/

            }

        }
        void YaSearch()
        {
            System.Net.WebClient wc = new System.Net.WebClient();

            wc.Encoding = Encoding.UTF8;
            var urlSearch = "https://yandex.ru/search/?text=";
            string url = urlSearch + HttpUtility.UrlEncode("такси дешево спб ");
            string xPathImageCaptcha = "//td[@class='b-captcha__layout__l']//img";

            string capth = "//div[@class=\"captcha__image\"]";
            string response = wc.DownloadString(url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(response);
            List<string> lsit = new List<string>();

            var main = doc.DocumentNode.SelectSingleNode(".//ul[@class=\"serp-list serp-list_left_yes\"]");
            HtmlNode image;
            string path;
            if (main == null)
            {
                image = doc.DocumentNode.SelectSingleNode(capth);
                path = image.InnerHtml;

            }
            var mw = doc.DocumentNode.SelectNodes("//div[@class=\"content__left\"]");
            // x => x.GetAttributeValue("data-5acg", "no").Equals("no")
            var sucs = main.ChildNodes.Where(x => x.Name == "li").ToList();

            foreach (var item in sucs)
            {

                var qw = item.ChildNodes[0].ChildNodes.Where(x => x.Name == "h2").First();
                var text = qw.ChildNodes.Where(x => x.Name == "a").First().ChildNodes.Where(x => x.Name == "div" && x.GetAttributeValue("class", "@").Equals("organic__url-text")).First();
                Console.WriteLine(text.InnerText);
                // if (!item.GetAttributeValue("data - 5acg","222").Equals("222"))
                //   Console.WriteLine("yes");
            }
        }
       
    }
}
