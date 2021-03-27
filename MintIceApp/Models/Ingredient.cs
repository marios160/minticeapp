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
        public int Quantity { get; set; }
        
        public Ingredient(string name, int quantity)
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
