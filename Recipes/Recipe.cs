using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Recipes
{
    [Table("Recipes")]
    public class Recipe
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public bool favorite { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string NamesOfIngredient { get; set; }
        public string Units { get; set; }
        public string Weights { get; set; }
        public string Instructions { get; set; }
        public int Time { get; set; }
        private TypeDish type = new TypeDish();
        public int Type {
            get 
            {
                return type.Type;
            }
            set
            {
                type.Type = value;
            }
        }

        public string Ingredients{
            get
            {
                string Ingredients = "";
                List<string> NameIngred = OutDataBaseArray(NamesOfIngredient, ',');
                List<string> Unit = OutDataBaseArray(Units, ',');
                List<string> Weight = OutDataBaseArray(Weights, '@');
                for (int i = 0; i < NameIngred.Count; i++)
                {
                    Ingredients += new Ingred(NameIngred[i], double.Parse(Weight[i]),
                       Unit[i]).Out();
                    if (i + 1 != NameIngred.Count)
                    {
                        Ingredients += "\n";
                    }
                }
                return Ingredients;
            }
        }

        public List<Ingred> IngredList
        {
            get
            {
                List<Ingred> OutPut = new List<Ingred>();
                List<string> NameIngred = OutDataBaseArray(NamesOfIngredient, ',');
                List<string> Unit = OutDataBaseArray(Units, ',');
                List<string> Weight = OutDataBaseArray(Weights, '@');
                for (int i = 0; i < NameIngred.Count; i++)
                {
                    OutPut.Add(new Ingred(NameIngred[i], double.Parse(Weight[i]),
                        Unit[i]));
                }
                return OutPut;
            }
        }
        public List<Instruct> Instructs
        {
            get
            {
                int id = 0;
                List<Instruct> OutPut = new List<Instruct>();
                List<string> Instructions = OutDataBaseArray(this.Instructions, '@');
                foreach(string str in Instructions)
                {
                    OutPut.Add(new Instruct(str, id));
                    id++;
                }
                return OutPut;
            }
        }
        public string OutInstruction()
        {
            List<string> Instructions = OutDataBaseArray(this.Instructions, '@');
            string str = "";
            int i = 1;
            foreach (string instruction in Instructions)
            {
                str += $"Шаг {i}:\n\n";
                i++;
                str += instruction + "\n\n";
            }
            return str;
        }

        protected List<string> OutDataBaseArray(string str, char separator)
        {
            List<string> temp = new List<string>();
            int i;
            string item = "";
            for (i = 0; i < str.Length; i++)
            {
                if (str[i] != separator)
                {
                    item += str[i];
                }
                else
                {
                    temp.Add(item);
                    item = "";
                }
            }
            temp.Add(item);
            return temp;
        }
        public Recipe(string Name, string PathImage, int Type, List<Ingred> Ingredients, 
                List<Instruct> Instruction, int Time)
        {
            this.Name = Name;
            ImageSource = PathImage;
            this.Type = Type;
            InDataBaseIngredArray(Ingredients);
            InDataBaseIstructionArray(Instruction);
            this.Time = Time;
        }

        public void InDataBaseIngredArray(List<Ingred> lst)
        {
            for(int i = 0; i < lst.Count; i++)
            {
                NamesOfIngredient += lst[i].NameOfIngredient;
                Weights += lst[i].Weight.ToString();
                Units += lst[i].Unit;
                if (i + 1 != lst.Count)
                {
                    NamesOfIngredient += ",";
                    Weights += "@";
                    Units += ",";
                }
            }
        }
        public void InDataBaseIstructionArray(List<Instruct> lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                Instructions += lst[i].Step;
                if (i + 1 != lst.Count)
                {
                    Instructions += "@";
                }
            }
        }
        public Recipe(Recipe item)
        {
            Name = item.Name;
            ImageSource = item.ImageSource;
            Type = item.Type;
            InDataBaseIngredArray(item.IngredList);
            InDataBaseIstructionArray(item.Instructs);
            Time = item.Time;
            favorite = item.favorite;
        }
        public Recipe() { }
      
    }
}
