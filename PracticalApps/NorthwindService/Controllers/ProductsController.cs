using Microsoft.AspNetCore.Mvc;
using Packt.Shared;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Northwind db;

        public ProductsController(Northwind db)
        {
            this.db = db;
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Product>))]
        public IEnumerable<Product> Get()
        {
            var products = db.Products.ToArray();
            return products;
        }

        [HttpGet("{id}", Name= nameof(GetProductsByCategoryId))]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Product>))]
        [ProducesResponseType(404)]
        public IEnumerable<Product> GetProductsByCategoryId(int id)
        {
            var products = db.Products.Where(product => product.CategoryID == id).ToArray();
            return products;
        }

    }
}