using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyDataAccess.Repositry.IRepositry
{
    internal interface IRepostiry<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetFirstOrDefault();
    }
}
