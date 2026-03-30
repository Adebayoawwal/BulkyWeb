using BulkyDataAccess.Repositry.IRepositry;
using BulkyWebModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyDataAccess.Repositry
{
    public class CategoryRepository : Repositry<Category>,ICategoryRepositry
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
