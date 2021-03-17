using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FlagsQuizApp.ViewModels;
using Xamarin.Forms.Xaml;
using FlagsQuizApp.Models;

namespace FlagsQuizApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryView : ContentPage
    {
        CountryViewModel vm;
        

        public CountryView()
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
    }
}