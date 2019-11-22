using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipePage : ContentPage
    {
        public RecipePage()
        {
            InitializeComponent();
        }
        private Recipe recipe;
        public RecipePage(Recipe recipe)
        {
            InitializeComponent();
            Animation();
            App.DataBaseFavorite.GetItems();
            InstructionList.ItemsSource = recipe.Instructs;
            NameRecipe.Text = recipe.Name;
            MainImage.Source = recipe.ImageSource;
            Ingredients.Text = recipe.Ingredients;
            Time.Text = recipe.Time + Resource.Min;
            Calorie.Text += GetCalorie(recipe).ToString() + Resource.Kcal;
            this.recipe=recipe;
            if(recipe.favorite == true)
            {
                ToolBarFavorite.Text = Resource.PageDelFavorite;
            }
            else
            {
                ToolBarFavorite.Text = Resource.PageAddFavorite;
            }
        }

        private async void Animation()
        {
            await Time.RotateTo(360, 2000, Easing.SinInOut);
        }
        private double GetCalorie(Recipe recipe)
        {
            double calorie = 0;
            Ingred ing;
            var LstIngred = new List<Ingred>();
            foreach(Ingred ingred in recipe.IngredList)
            {
                ing = App.DatabaseIngred.GetItem(ingred.NameOfIngredient);
                if(ing != null)
                {
                    LstIngred.Add(ing);
                    if (ingred.Unit[0] == LstIngred.Last().Unit[0])
                    {
                        calorie += LstIngred.Last().Calorie * ingred.Weight/ LstIngred.Last().Weight;
                    }
                }
            }
            return calorie;
        }


        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (recipe.favorite != true)
            {
                recipe.favorite = true;
                ContainerOfFavorite.SaveItem(new Favorite(JsonConvert.SerializeObject(recipe)));
                App.DatabaseRecipe.ChangeItem(recipe);
                ToolBarFavorite.Text = Resource.PageDelFavorite;
            }
            else
            {
                App.DataBaseFavorite.DeleteItem(JsonConvert.SerializeObject(recipe));
                recipe.favorite = false;
                ToolBarFavorite.Text = Resource.PageAddFavorite;
            }
        }
    }
}