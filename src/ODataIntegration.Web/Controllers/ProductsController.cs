using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using ODataIntegration.Data;
using ODataIntegration.Data.Entities;
using ODataIntegration.Data.Extensions;

namespace ODataIntegration.Web.Controllers
{
    public class ProductsController : ODataController
    {
        private AppDbContext db;

        public ProductsController(AppDbContext db)
        {
            this.db = db;
        }

        [EnableQuery]
        public async Task<List<Product>> Get() => await db.GetProducts();
    }
}