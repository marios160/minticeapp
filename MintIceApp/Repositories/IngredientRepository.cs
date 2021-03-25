using MintIceApp.Models;
using MintIceApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MintIceApp.Repositories
{
    class IngredientRepository
    {
        internal static int Insert(Ingredient Ingredient)
        {
            int result = DataBase.db.UpdateAsync(Ingredient).Result;
            if (result == 0)
                result = DataBase.db.InsertAsync(Ingredient).Result;
            return result;
        }

        internal static Task<List<Ingredient>> FindAll()
        {
            return DataBase.db.Table<Ingredient>().ToListAsync();
        }

        internal static Task<List<Ingredient>> FindAllByRecipeId(int id)
        {
            return DataBase.db.Table<Ingredient>().Where(r => r.RecipeId == id).ToListAsync();
        }

        internal static Task<Ingredient> FindOneById(int id)
        {
            return DataBase.db.Table<Ingredient>().Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        internal static Task<List<Ingredient>> FindByName(string name)
        {
            return DataBase.db.Table<Ingredient>().Where(r => r.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        internal static bool Delete(Ingredient r)
        {
            int rows = DataBase.db.DeleteAsync(r).Result;
            if (rows == 0)
                return false;
            return true;
        }

        internal static bool Delete(int id)
        {
            Ingredient r = FindOneById(id).Result;
            int rows = DataBase.db.DeleteAsync(r).Result;
            if (rows == 0)
                return false;
            return true;
        }
    }
}
