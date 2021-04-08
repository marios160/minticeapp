using MintIceApp.Models;
using SQLite;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

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
            //db.DropTableAsync<Recipe>();
            //db.DropTableAsync<Product>();
            //db.DropTableAsync<Ingredient>();
            db.CreateTableAsync<Recipe>();
            db.CreateTableAsync<Product>();
            db.CreateTableAsync<Ingredient>();
        }

        static async public Task clearDB()
        {
            await db.DropTableAsync<Recipe>();
            await db.DropTableAsync<Product>();
            await db.DropTableAsync<Ingredient>();
            await db.CreateTableAsync<Recipe>();
            await db.CreateTableAsync<Product>();
            await db.CreateTableAsync<Ingredient>();
        }
    }
}
