using Microsoft.EntityFrameworkCore;
using MoneyManager.Data.Entities;
using Entry = MoneyManager.Data.Entities.Entry;

namespace MoneyManager.Data
{
    public class Database : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Entry> Entries { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "app.db");
            optionsBuilder.UseSqlite($"Filename = {dbPath}");
        }
    }
}