using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseSearch.Repository
{
    public class RepositoryContext : DbContext
    {
        private string _databasePath;


        public DbSet<InstrumentNomenclature> InstrumentNomenclatures { get; set; }
        public DbSet<MaterialNomenclature> MaterialNomenclatures { get; set; }
        public DbSet<InstrumnetHeader> InstrumentHeaders { get; set; }

        public DbSet<ElementInstrumentToUpload> InventoryListInstrumentToUpdate { get; set; }
        public DbSet<ElementMaterialToUpload> InventoryListMaterialToUpdate { get; set; }


        public DbSet<InventoryObject> InventoryObjects { get; set; }

        public DbSet<User> Users { get; set; }
        public AppDataBaseContext(string databasePath)
        {
            _databasePath = databasePath;
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite($"Data Source={_databasePath}");
        }
    }
