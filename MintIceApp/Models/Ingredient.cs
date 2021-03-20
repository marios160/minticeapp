using System;
using System.Collections.Generic;
using System.Text;

namespace MintIceApp.Models
{
    public class Ingredient
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }

        public Ingredient(int recipeId, string name, decimal quantity)
        {
            RecipeId = recipeId;
            Name = name;
            Quantity = quantity;
        }

        public Ingredient()
        {
        }
    }
}
