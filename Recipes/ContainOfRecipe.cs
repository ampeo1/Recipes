using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Text;
using SQLite;
using System.Linq;

namespace Recipes
{
    public class  ContainOfRecipe
    {
        static SQLiteConnection database;
        
        public ContainOfRecipe(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.Table<Recipe>();
        }

        public IEnumerable<Recipe> GetItems()
        {
            return (from i in database.Table<Recipe>() select i).ToList();
        }

        public int DeleteItem(int id)
        {
            return database.Delete<Recipe>(id);
        }

        public void ChangeItem(Recipe item)
        {
            DeleteItem(item.Id);
            SaveItem(new Recipe(item));
        }

        static public int SaveItem(Recipe item)
        {
            if(item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
