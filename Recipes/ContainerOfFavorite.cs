using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Recipes
{
    public class ContainerOfFavorite
    {
        static SQLiteConnection database;

        public ContainerOfFavorite(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            if (database.Table<Favorite>() == null)
            {
                database.CreateTable<Favorite>();
            }
        }

        public IEnumerable<Favorite> GetItems()
        {
            return (from i in database.Table<Favorite>() select i).ToList();
        }

        public int DeleteItem(int id)
        {
            return database.Delete<Favorite>(id);
        }
        public void DeleteItem(string json)
        {
            int i;
            Favorite[] array = database.Table<Favorite>().ToArray();
            for (i = 0; i < array.Length; i++)
            {
                if (array[i].json == json)
                {
                    database.Delete<Favorite>(array[i].id);
                }
            }
        }
        static public int SaveItem(Favorite item)
        {
            if (item.id != 0)
            {
                database.Update(item);
                return item.id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
