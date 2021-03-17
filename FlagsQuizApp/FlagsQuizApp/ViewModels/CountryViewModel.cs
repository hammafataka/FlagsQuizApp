using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FlagsQuizApp.Models;

using Xamarin.Forms;

using FlagsQuizApp.Services;


namespace FlagsQuizApp.ViewModels
{
    public class CountryViewModel:BaseViewModel
    {
    

        private List<Country> countries;

        private int points = 10;

        private Country currentCountry;
        public Country CurrentCountry
        {
            get => currentCountry;
            set { currentCountry = value; OnPropertyChange(); }
        }

        private string currentFlagUrl;
        public string CurrentFlagUrl
        {
            get => currentFlagUrl;
            set { currentFlagUrl = value; OnPropertyChange(); }
        }

        private string answerResult;
        public string AnswerResult
        {
            get => answerResult;
            set { answerResult = value; OnPropertyChange(); }
        }

        private int score;
        public int Score
        {
            get => score;
            set { score = value; OnPropertyChange(); }
        }

        public ObservableCollection<Country> CountryOptions { get; }

        private Country selectedCountry;

        public Country SelectedCountry
        {
            get => selectedCountry;
            set { selectedCountry = value; OnPropertyChange(); }
        }

        private readonly Random randomGenerator;

        private int numberOfOptions = 4;

        public ICommand LoadDataCommand { private set; get; }

        public ICommand EvaluateChoiceCommand { private set; get; }

        private async Task LoadData()
        {
            countries = await FlagApiService.GetCountries();
            await CreateQuestion();
        }

        private async Task CreateQuestion()
        {
            IsBusy = true;

            int numberOfCountries = countries.Count;

            if (numberOfCountries > 0)
            {
                await Task.Delay(3000);

                int rightAnswerIndex = randomGenerator.Next(0, numberOfOptions);

                List<Country> questionOptions = new List<Country>();

                for (int i = 0; i < numberOfOptions; i++)
                {
                    int index = randomGenerator.Next(0, numberOfCountries);

                    Country country = countries[index];

                    if (!questionOptions.Any(x => x.GeoNameId == country.GeoNameId))
                    {
                        questionOptions.Add(country);

                        if (i == rightAnswerIndex)
                        {
                            CurrentCountry = country;

                            string countryCode = CurrentCountry.CountryCode.ToLower();
                            CurrentFlagUrl = $"https://raw.githubusercontent.com/hjnilsson/country-flags/master/png250px/{countryCode}.png";
                        }
                    }
                    else
                    {
                        i--;
                    }
                }

                CountryOptions.Clear();

                foreach (Country option in questionOptions)
                    CountryOptions.Add(option);

                AnswerResult = string.Empty;
            }

            IsBusy = false;
        }

        private async Task EvaluateChoice()
        {
            if (!IsBusy && SelectedCountry != null)
            {
                if (CurrentCountry.GeoNameId == SelectedCountry.GeoNameId)
                {
                    Score += points;
                    AnswerResult = "Correct! =)";
                }
                else
                {
                    Score -= (points / 2);
                    AnswerResult = "Wrong! =(";
                }

                await CreateQuestion();
            }
        }

        public CountryViewModel()
        {
            randomGenerator = new Random();

            CurrentCountry = new Country() { CountryCode = string.Empty };

            CountryOptions = new ObservableCollection<Country>();

            LoadDataCommand = new Command(async () => await LoadData());
            EvaluateChoiceCommand = new Command(async () => await EvaluateChoice());
        }
        
        
        
        
        
        
    }


}

