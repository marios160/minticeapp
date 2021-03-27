using System;
using System.Collections.Generic;
using System.Text;
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
            this.CreatedAt = new DateTime();
            this.UpdatedAt = new DateTime();
            this.Favourite = false;
        }
    }
}
