using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace FlagsQuizApp.ViewModels 
{
      
    public class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange([CallerMemberName]string PropertyName="")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(PropertyName));

        }
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set { isBusy = value; OnPropertyChange(); }
        }

    }
}
