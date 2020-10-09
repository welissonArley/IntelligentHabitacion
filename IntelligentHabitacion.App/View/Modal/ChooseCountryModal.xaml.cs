using IntelligentHabitacion.Useful;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View.Modal
{
    [DesignTimeVisible(false)]
    public partial class ChooseCountryModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly List<CountryModel> _listCountry;
        private readonly ICommand _onCountrySelectedCommand;

        public ChooseCountryModal(ICommand onCountrySelectedCommand)
        {
            InitializeComponent();

            _listCountry = Country.Countries;
            List.ItemsSource = new ObservableCollection<CountryModel>(_listCountry);
            SearchByName.EntryTextChanged = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });

            _onCountrySelectedCommand = onCountrySelectedCommand;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            ((ListView)sender).BackgroundColor = Xamarin.Forms.Color.Transparent;
            Navigation.PopPopupAsync();
            _onCountrySelectedCommand.Execute(e.Item);
        }

        private void OnSearchTextChanged(string value)
        {
            List.ItemsSource = new ObservableCollection<CountryModel>(_listCountry.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());
        }
    }
}
