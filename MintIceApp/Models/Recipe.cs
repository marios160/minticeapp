using System;
using System.Collections.Generic;
using System.Text;
using MintIceApp.Repositories;
using SQLite;

namespace MintIceApp.Models
{
    public class Recipe
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Favourite { get; set; }

        public Recipe()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Favourite = false;
        }

        public List<Ingredient> GetIngredients()
        {
            return IngredientRepository.FindAllByRecipeId(Id).Result;
        }
    }
}
