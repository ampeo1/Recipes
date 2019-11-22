using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecipe : ContentPage
    {
        private  MediaFile img = null;
        private Image photo = new Image();
        private int id = 0;
        private ObservableCollection<Instruct> Instruction { get; set; }
        private ObservableCollection<Ingred> Ingredients { get; set; }
        public AddRecipe()
        {
            InitializeComponent();
            Instruction = new ObservableCollection<Instruct>();
            Ingredients = new ObservableCollection<Ingred>();
            IngredientsList.ItemsSource = Ingredients;
            InstructionList.ItemsSource = Instruction;
            TypeDishPicker.ItemsSource = TypeDish.NameTypeDish;
            TypeDishPicker.SelectedItem = TypeDish.NameTypeDish[5];
            this.BindingContext = this;
        }

        private async void AddImage(object sender, EventArgs e) 
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                img = await CrossMedia.Current.PickPhotoAsync();
                photo.Source = ImageSource.FromFile(img.Path);
                ViewImage.Source = photo.Source;
            }

        }

        private bool CheckStep()
        {
            if (EditStep.Text != "" && EditStep.Text != null)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        private void AddStep(object sender, EventArgs e)
        {
            if (CheckStep() == true)
            {
                Instruction.Add(new Instruct(EditStep.Text, id));
                id++;
                EditStep.Text = "";
            }
            else
            {
                DisplayAlert(Resource.Error, Resource.ErrorStep, Resource.Ok);
            }
        }

        private void AddIngred(object sender, EventArgs e) // проверку на число веса сделать
        {
            if(double.TryParse(Weight.Text,out double weight) != true)
            {
                DisplayAlert(Resource.Error, Resource.ErrorWeight, Resource.Ok);
                return;
            }

            if (Ingred.Text != null && Ingred.Text != "")
                if (Unit.Text != null && Unit.Text != "")
                {
                    Ingredients.Add(new Ingred(Ingred.Text, weight, Unit.Text));
                }
                else
                {
                    Ingredients.Add(new Ingred(Ingred.Text, weight, "грамм"));
                }
            else
                DisplayAlert(Resource.Error, Resource.ErrorIngred, Resource.Ok);
            Ingred.Text = "";
            Unit.Text = "";
            Weight.Text = "";
            Unit.IsReadOnly = false;
        }

        private bool Check()
        {
            if (NameRecipe.Text == null || NameRecipe.Text == "")
            {
                DisplayAlert(Resource.Error, Resource.ErrorName, Resource.Ok);
                return false;
            }
            if (photo.Source == null)
                photo.Source = "LoadImage.png";
            if (Ingredients.Count == 0)
            {
                DisplayAlert(Resource.Error, Resource.ErrorNullIngred, Resource.Ok);
                return false;
            }
            if (Instruction.Count == 0)
            {
                DisplayAlert(Resource.Error, Resource.ErrorNullStep, Resource.Ok);
                return false;
            }

            if (int.TryParse(Time.Text, out _) != true)
            {
                DisplayAlert(Resource.Error, Resource.ErrorTime, Resource.Ok);
                return false;
            }
            return true;
        }

        private void Add(object sender, EventArgs e)
        {
            if(Check() == true && img != null)
            {
                Recipe NewRecipe = new Recipe(NameRecipe.Text, img.Path, TypeDishPicker.SelectedIndex,
                    Ingredients.ToList(), Instruction.ToList(), int.Parse(Time.Text));
                ContainOfRecipe.SaveItem(NewRecipe);
            }
            if (Check() == true && img == null)
            {
                Recipe NewRecipe = new Recipe(NameRecipe.Text, "LoadImage.png", TypeDishPicker.SelectedIndex,
                    Ingredients.ToList(), Instruction.ToList(), int.Parse(Time.Text));
                ContainOfRecipe.SaveItem(NewRecipe);
            }
        }

        private void DeleteIngred(object sender, EventArgs e)
        {
            if (IngredientsList.SelectedItem is Ingred ingred)
            {
                Ingredients.Remove(ingred);
                IngredientsList.SelectedItem = null;
            }


        }

        private void DeleteInstruction(object sender, EventArgs e)
        {
            int i;
            if (InstructionList.SelectedItem is Instruct instruct)
            {
                for (i = instruct.Id + 1; i < Instruction.Count; i++)
                {
                    Instruction[i].ChangeId();
                }
                id--;
                Instruction.Remove(instruct);
                InstructionList.SelectedItem = null;

            }
        }
        private void ChangeInstruction(object sender, EventArgs e)
        {
            if (InstructionList.SelectedItem is Instruct instruct && CheckStep() == true)
            {
                Instruction.Remove(instruct);
                InstructionList.SelectedItem = null;
                instruct.Step = EditStep.Text;
                EditStep.Text = "";
                Instruction.Insert(instruct.Id, instruct);
            }
            else
            {
                DisplayAlert(Resource.Error, Resource.ErrorStep, Resource.Ok);
            }
        }

        private void TypeDishPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeDishLabel.Text = Resource.AddTypeDish + TypeDishPicker.SelectedItem;
        }

        private void Ingred_TextChanged(object sender, TextChangedEventArgs e)
        {
            IEnumerable<Ingred> ingredient = (from ing in App.DatabaseIngred.GetItems()
                                         where ing.NameOfIngredient == Ingred.Text
                                         select ing);
            if(ingredient.Count() != 0)
            {
                Unit.Text = ingredient.First().Unit;
                Unit.IsReadOnly = true;
            }

        }
    }
}