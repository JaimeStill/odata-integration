using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ODataIntegration.Data.Entities;

namespace ODataIntegration.Data.Extensions
{
    public static class DbInitializer
    {
        public static async Task Initialize(this AppDbContext db)
        {
            Console.WriteLine("Initializing database");
            await db.Database.MigrateAsync();
            Console.WriteLine("Database initialized");

            if (!await db.Products.AnyAsync())
            {
                Console.WriteLine("Seeding products...");

                var products = new List<Product>
                {
                    new Product { Name = "Bread" },
                    new Product { Name = "Milk" },
                    new Product { Name = "Eggs" },
                    new Product { Name = "Sugar" },
                    new Product { Name = "Coffee" }
                };

                await db.Products.AddRangeAsync(products);
                await db.SaveChangesAsync();
            }
        }
    }
}