using ParseSearch.Model;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.Repository
{
    public class LocalRepository : DbContext
    {
        


         public DbSet<SearchResult> SearchResults { get; set; }


        public LocalRepository()
         : base("DefaultConnection") 
        {
             //Database.Delete();
            Database.CreateIfNotExists();
        }

       

    }
}
