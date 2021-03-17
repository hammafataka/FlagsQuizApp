using FlagsQuizApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlagsQuizApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CountryView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
