using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductSearch : ContentPage
    {
        private ObservableCollection<Ingred> Ingredients { get; set; }
        private ObservableCollection<Recipe> Rec { get; set; }
        public ProductSearch()
        {
            InitializeComponent();
            Ingredients = new ObservableCollection<Ingred>();
            IngredientsList.ItemsSource = Ingredients;
            Rec = new ObservableCollection<Recipe>();
            foreach(Recipe rec in App.DatabaseRecipe.GetItems())
            {
                Rec.Add(rec);
            }
            RecipesList.ItemsSource = Rec;
        }

        private void AddIngred(object sender, EventArgs e) // проверку на число веса сделать
        {

            if (Ingred.Text != null && Ingred.Text != "")
            { 
                    Ingredients.Add(new Ingred(Ingred.Text, 0, ""));
            }
            else
                DisplayAlert("Ошибка", "Вы не ввели имя ингредиента", "ОК");
            Ingred.Text = "";
        }

        private void DeleteIngred(object sender, EventArgs e)
        {
            if (IngredientsList.SelectedItem is Ingred ingred)
            {
                Ingredients.Remove(ingred);
                IngredientsList.SelectedItem = null;
            }
        }
        private void IngredientsList_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            int i, j;
            List<Recipe> recipes = App.DatabaseRecipe.GetItems().ToList();
            List<Recipe> result = new List<Recipe>();
            for(i = 0; i < recipes.Count; i++)
            {
                for(j = 0; j < recipes[i].IngredList.Count; j++)
                {
                    if(check(recipes[i].IngredList[j]) == true)
                    {
                        result.Add(recipes[i]);
                        break;
                    }

                }
            }
            RecipesList.ItemsSource = result;
        }

        private bool check (Ingred ing)
        {
            int i;
            for(i = 0; i < Ingredients.Count; i++)
            {
                if (ing.NameOfIngredient == Ingredients[i].NameOfIngredient)
                {
                    return true;
                }
            }
            return false;
        }
        private async void RecipesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new RecipePage((Recipe)e.Item));
        }
    }
}
