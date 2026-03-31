using BulkyWebModels.Models;
using NuGet.Packaging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyDataAccess.Repositry.IRepositry
{
    public interface ICategoryRepositry :IRepostiry<Category>
    {
        void Update(Category category);
        void Save();
    }
}
