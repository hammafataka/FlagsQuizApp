using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FlagsQuizApp.ViewModels;
using Xamarin.Forms.Xaml;


namespace FlagsQuizApp
{
    public partial class MainPage : ContentPage
    {
        CountryViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            vm = new CountryViewModel();
            BindingContext = vm;

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Run(() => vm.LoadDataCommand.Execute(null));
        }

        private void ResetBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}
