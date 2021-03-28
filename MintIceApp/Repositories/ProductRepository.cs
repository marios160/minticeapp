using MintIceApp.Models;
using MintIceApp.Services;
using System;
using System.Collections.Generic;
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
            return DataBase.db.Table<Product>().ToListAsync();
        }
        internal static Task<List<Product>> FindAllByCreatedAt()
        {
            return DataBase.db.Table<Product>().Where(p => p.CreatedAt.ToString("d") == DateTime.Now.ToString("d")).ToListAsync();
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
