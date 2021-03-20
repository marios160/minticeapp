using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MintIceApp.Models
{
    public class Product
    {
        [PrimaryKey, Unique, AutoIncrement]
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Recipe Recipe { get; set; }

        public Product(decimal quantity, string name)
        {
            Quantity = quantity;
            Name = name;
            CreatedAt = new DateTime();
            UpdatedAt = new DateTime();
        }

        public Product()
        {
            CreatedAt = new DateTime();
            UpdatedAt = new DateTime();
        }
    }
}
