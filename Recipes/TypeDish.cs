using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes
{
    public class TypeDish
    {
        public static readonly string[] NameTypeDish = {Resource.TypeDishFirst, Resource.TypeDishSecond,
            Resource.TypeDishSalad,Resource.TypeDishBreakfast,Resource.TypeDishDrink,Resource.TypeDishOther};
        public TypeDish() 
        {
            type = Kind.NaN;
        }
        public enum Kind{
            First,
            Second,
            Salad,
            Breakfast,
            Drink,
            NaN
        }

        private Kind type;

        public int Type
        {
            set
            {
                if(value < 6 && value >= 0)
                    type = (Kind)value;
            }
            get
            {
                return (int)type;
            }
        }

    }
}
