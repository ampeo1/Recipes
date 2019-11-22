using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace Recipes
{
    [Table("Ingred")]
    public class Ingred
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string NameOfIngredient { get; set; }
        public double Weight { get; set; }
        public string Unit { get; set; }

        public double Calorie { get; set; }
        public Ingred(string name, double Weight, string Unit)
        {
            this.NameOfIngredient = name;
            this.Weight = Weight;
            this.Unit = Unit;
        }

        public string Out()
        {
            string OutString;
            if (Weight == 0)
                OutString = NameOfIngredient + ": " + Unit;
            else
                OutString = NameOfIngredient + ": " + Weight + " " + Unit;
            return OutString;
        }
        public Ingred(){ }
        

    }
}
