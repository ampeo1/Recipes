using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes
{
    [Table("Favorite")]
    public class Favorite
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int id { get; set; }
        public string json { get; set; }

        //private static int Id = 0;
        public Favorite(string json)
        {
            this.json = json;
          //  this.id = Id;
          //  Id++;
        }

        public Favorite(){}
    }
}
