using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ODataIntegration.Data.Entities;

namespace ODataIntegration.Data.Extensions
{
    public static class ProductExtensions
    {
        public static async Task<List<Product>> GetProducts(this AppDbContext db) =>
            await db.Products.ToListAsync();
    }
}