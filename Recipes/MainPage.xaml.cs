using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Recipes
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private const string ColorTextBar = "#515383";
        private const string ColorBackgroundBar = "#BC9E6F";
        public MainPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new Catalog())
            {
                BarTextColor = Color.FromHex(ColorTextBar),
                BarBackgroundColor = Color.FromHex(ColorBackgroundBar)

            };
        }

        private void Catalog_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Catalog())
            { 
                BarTextColor = Color.FromHex(ColorTextBar),
                BarBackgroundColor = Color.FromHex(ColorBackgroundBar)
            };

            IsPresented = false;
        }


        private void Add_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AddRecipe())
            {
                BarTextColor = Color.FromHex(ColorTextBar),
                BarBackgroundColor = Color.FromHex(ColorBackgroundBar)
            }; 

            IsPresented = false;
        }

        private void Search_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new ProductSearch())
            {
                BarTextColor = Color.FromHex(ColorTextBar),
                BarBackgroundColor = Color.FromHex(ColorBackgroundBar)
            };

            IsPresented = false;
        }

    }
}
