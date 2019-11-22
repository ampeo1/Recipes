using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipes
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "Recipes.db";
        private static ContainOfRecipe databaseRecipe;
        public static ContainOfRecipe DatabaseRecipe
        {
            get
            {
                if (databaseRecipe == null)
                    databaseRecipe = new ContainOfRecipe(DATABASE_NAME);
                return databaseRecipe;
            }
        }
        private static ContainOfIngred databaseIngred;
        public static ContainOfIngred DatabaseIngred
        {
            get
            {
                if (databaseIngred == null)
                    databaseIngred = new ContainOfIngred(DATABASE_NAME);
                return databaseIngred;
            }
        }

        private static ContainerOfFavorite databaseFavorite;
        public static ContainerOfFavorite DataBaseFavorite
        {
            get
            {
                if (databaseFavorite == null)
                    databaseFavorite = new ContainerOfFavorite(DATABASE_NAME);
                return databaseFavorite;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
