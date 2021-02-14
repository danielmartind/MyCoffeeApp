using MvvmHelpers;
using MvvmHelpers.Commands;
using MyCoffeeApp.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyCoffeeApp.ViewModels
{
    public class CoffeeEquipmentViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Coffee>> CoffeeGroups { get; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Coffee> FavouriteCommand { get; }

        public CoffeeEquipmentViewModel()
        {
            Title = "Coffee Equipment";

            Coffee = new ObservableRangeCollection<Coffee>();
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Coffee>>();

            var image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png";

            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Sip of Sunshine", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Potent Potable", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Handege", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Handege", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Handege", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Handege", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Handege", Image = image });

            CoffeeGroups.Add(new Grouping<string, Coffee>("Blue Bottle", new[] { Coffee[2] }));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Plz", Coffee.Take(2)));

            RefreshCommand = new AsyncCommand(Refresh);
            FavouriteCommand = new AsyncCommand<Coffee>(Favorite);
        }

        private async Task Favorite(Coffee coffee)
        {
            if(coffee==null)
                return;
           await Application.Current.MainPage.DisplayAlert("Favourite", coffee.Name, "OK");
        }

        private async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            IsBusy = false;
        }

        private Coffee _previouslySelected;
        private Coffee _selectedCoffee;

        public Coffee SelectedCoffee
        {
            get => _selectedCoffee;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "OK");
                    _previouslySelected = value;
                    value = null;
                }

                _selectedCoffee = value;
                OnPropertyChanged();
            }
        }
    }
}