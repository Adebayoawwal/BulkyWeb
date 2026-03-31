using BulkyDataAccess.Repositry.IRepositry;
using BulkyModels.Models;
using BulkyWebModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyDataAccess.Repositry
{
    public class ProductRepositry : Repositry<Product>, IProductRepositry
    {
        private ApplicationDbContext _db;
        public ProductRepositry(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        public void Update(Product category)
        {
            _db.Products.Update(category);
        }
    }
}
