using MintIceApp.Models;
using SQLite;
using System;
using System.Diagnostics;
using System.IO;

namespace MintIceApp.Services
{
    class DataBase
    {
        public static string DbPath { get; set; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "minticeapp.db3");
        public static SQLiteAsyncConnection db { get; set; }
        public DataBase()
        {
            db = new SQLiteAsyncConnection(DbPath);
            //db.DropTable<Recipe>();
            //db.DropTable<Product>();
            //db.DropTable<Ingredient>();
            db.CreateTableAsync<Recipe>();
            db.CreateTableAsync<Product>();
            db.CreateTableAsync<Ingredient>();
        }
    }
}
