using BulkyDataAccess.Repositry.IRepositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyDataAccess.Repositry
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext _db;
         public ICategoryRepositry Category{ get; private set; }
        public IProductRepositry Product { get; private set; }
        public UnitOfWork (ApplicationDbContext db)
        {
            _db = db;
            Category =new CategoryRepository(_db);
            Product = new ProductRepositry(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
