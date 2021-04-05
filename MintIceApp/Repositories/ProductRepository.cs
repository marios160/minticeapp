using MintIceApp.Models;
using MintIceApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MintIceApp.Repositories
{
    class ProductRepository
    {
        internal static int Insert(Product Product)
        {
            int result = DataBase.db.UpdateAsync(Product).Result;
            if (result == 0)
                result = DataBase.db.InsertAsync(Product).Result;
            return result;
        }

        internal static Task<List<Product>> FindAll()
        {
            return DataBase.db.Table<Product>().OrderBy(p => p.CreatedAt).ToListAsync();
        }
        internal static Task<List<Product>> FindAllByCreatedAt()
        {
            DateTime start = DateTime.Today.Date + new TimeSpan(0, 0, 0);
            DateTime stop = DateTime.Today.Date + new TimeSpan(59, 59, 59);
            return DataBase.db.Table<Product>().Where(p => p.CreatedAt >= start && p.CreatedAt <= stop).OrderBy(p => p.CreatedAt).ToListAsync();
        }
        internal static Task<List<Product>> FindAllByFiltering(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.Date + new TimeSpan(23, 59, 59);
            dateFrom = dateFrom.Date + new TimeSpan(0, 0, 0);
            return DataBase.db.Table<Product>().Where(p => (p.CreatedAt >= dateFrom) && (p.CreatedAt <= dateTo)).OrderBy(p => p.CreatedAt).ToListAsync();
        }

        internal static Task<Product> FindOneById(int id)
        {
            return DataBase.db.Table<Product>().Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        internal static Task<List<Product>> FindByName(string name)
        {
            return DataBase.db.Table<Product>().Where(r => r.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        internal static bool Delete(Product r)
        {
            int rows = DataBase.db.DeleteAsync(r).Result;
            if (rows == 0)
                return false;
            return true;
        }

        internal static bool Delete(int id)
        {
            Product r = FindOneById(id).Result;
            int rows = DataBase.db.DeleteAsync(r).Result;
            if (rows == 0)
                return false;
            return true;
        }
    }
}
