using MintIceApp.Models;
using MintIceApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MintIceApp.Repositories
{
    class RecipeRepository
    {
        internal static int Insert(Recipe Recipe)
        {
            int result = DataBase.db.UpdateAsync(Recipe).Result;
            if (result == 0)
                result = DataBase.db.InsertAsync(Recipe).Result;
            return result;
        }

        internal static Task<List<Recipe>> FindAll()
        {
            return DataBase.db.Table<Recipe>().OrderByDescending(r => r.Favourite).ToListAsync();
        }

        internal static Task<Recipe> FindOneById(int id)
        {
            return DataBase.db.Table<Recipe>().Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        internal static Task<List<Recipe>> FindByName(string name)
        {
            return DataBase.db.Table<Recipe>().Where(r => r.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        internal static bool Delete(Recipe r)
        {
            int rows = DataBase.db.DeleteAsync(r).Result;
            if (rows == 0)
                return false;
            return true;
        }

        internal static bool Delete(int id)
        {
            Recipe r = FindOneById(id).Result;
            int rows = DataBase.db.DeleteAsync(r).Result;
            if (rows == 0)
                return false;
            return true;
        }
    }
}
