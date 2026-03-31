using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyDataAccess.Repositry.IRepositry
{
    public interface IUnitOfWork
    {
        ICategoryRepositry Category{ get; }
        IProductRepositry Product { get; }

        void Save();
    }
}
