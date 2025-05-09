using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;

namespace StockApp.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StockContext>
    {
        public StockContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StockContext>();
            
            // Get a suitable location for the database file
            var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StockApp");
            Directory.CreateDirectory(appDataPath);
            var dbPath = Path.Combine(appDataPath, "stockapp.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new StockContext(optionsBuilder.Options);
        }
    }
} 