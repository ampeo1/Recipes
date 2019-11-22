using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Catalog :  ContentPage
    {
        public Catalog()
        {
            InitializeComponent();
            Searchbar.Placeholder = Resource.CtalogSearch;
            RecipesList.ItemsSource = App.DatabaseRecipe.GetItems();
            this.BindingContext = this;
        }

        private async void RecipesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new RecipePage((Recipe)e.Item));
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = Searchbar.Text;
            RecipesList.ItemsSource = from str in App.DatabaseRecipe.GetItems().ToList()
                                      where str.Name.StartsWith(s)
                                      select str;
        }
        private void Sort(TypeDish.Kind type)
        {
            RecipesList.ItemsSource = from recipe in App.DatabaseRecipe.GetItems().ToList()
                                      where recipe.Type == (int)type
                                      select recipe;
        }
        private void ToolbarItem_Clicked_First(object sender, EventArgs e)
        {
            Sort(TypeDish.Kind.First);
        }

        private void ToolbarItem_Clicked_Second(object sender, EventArgs e)
        {
            Sort(TypeDish.Kind.Second);
        }

        private void ToolbarItem_Clicked_Salad(object sender, EventArgs e)
        {
            Sort(TypeDish.Kind.Salad);
        }

        private void ToolbarItem_Clicked_Breakfast(object sender, EventArgs e)
        {
            Sort(TypeDish.Kind.Breakfast);
        }

        private void ToolbarItem_Clicked_Drink(object sender, EventArgs e)
        {
            Sort(TypeDish.Kind.Drink);
        }

        private void ToolbarItem_Clicked_Nan(object sender, EventArgs e)
        {
            RecipesList.ItemsSource = null;
            RecipesList.ItemsSource = App.DatabaseRecipe.GetItems();
        }
        private void ToolbarItem_Clicked_Favorite(object sender, EventArgs e)
        {
            int i;
            Favorite[] favorites = App.DataBaseFavorite.GetItems().ToArray();
            List<Recipe> recipes = new List<Recipe>();
            for(i = 0; i < favorites.Length; i++)
            {
                Recipe rec = JsonConvert.DeserializeObject<Recipe>(favorites[i].json);
                recipes.Add((Recipe)rec);
            }
            RecipesList.ItemsSource =recipes;
        }
    }
}