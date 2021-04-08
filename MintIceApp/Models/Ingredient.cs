using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MintIceApp.Models
{
    public class Ingredient
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        
        public Ingredient(string name, decimal quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public Ingredient()
        {
            Quantity = 0;
        }
    }
}
