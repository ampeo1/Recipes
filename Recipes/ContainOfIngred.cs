using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Recipes
{
    public class ContainOfIngred
    {
        static SQLiteConnection databaseIngred;

        public ContainOfIngred(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            databaseIngred = new SQLiteConnection(databasePath);
            databaseIngred.Table<Ingred>();
        }

        public IEnumerable<Ingred> GetItems()
        {
            return (from i in databaseIngred.Table<Ingred>() select i).ToList();
        }

        public Ingred GetItem(string Name)
        {
            int i = 0;
            List<Ingred> ing = (from t in databaseIngred.Table<Ingred>()
                          select t).ToList();
            for(i = 0; i < ing.Count; i++)
            {
                if (ing[i].NameOfIngredient == Name)
                    return ing[i];
            }
            return null;

        }
       

        public int DeleteItem(int id)
        {
            return databaseIngred.Delete<Ingred>(id);
        }
        static public int SaveItem(Ingred item)
        {
            if (item.Id != 0)
            {
                databaseIngred.Update(item);
                return item.Id;
            }
            else
            {
                return databaseIngred.Insert(item);
            }
        }
    }
}
