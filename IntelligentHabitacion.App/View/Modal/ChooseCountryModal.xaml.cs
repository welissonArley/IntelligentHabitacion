using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Useful;
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
        private readonly IList<CountryModel> _listCountry;
        private readonly ICommand _onCountrySelectedCommand;

        public ChooseCountryModal(ICommand onCountrySelectedCommand)
        {
            InitializeComponent();

            SearchByName.EntryTextChanged = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });

            _listCountry = new CoutriesAvaliables().Get();

            _onCountrySelectedCommand = onCountrySelectedCommand;

            List.ItemsSource = new ObservableCollection<CountryModel>(_listCountry);
            List.SelectionChanged += OnCollectionViewSelectionChanged;
            AmountItens();
        }

        private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountryModel current = (e.CurrentSelection.FirstOrDefault() as CountryModel);
            Navigation.PopPopupAsync();
            _onCountrySelectedCommand.Execute(current);
        }

        private void OnSearchTextChanged(string value)
        {
            List.ItemsSource = new ObservableCollection<CountryModel>(_listCountry.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());
            AmountItens();
        }

        private void AmountItens()
        {
            var itens = (ObservableCollection<CountryModel>)List.ItemsSource;
            LabelAmountItens.Text = string.Format(ResourceText.TITLE_COUNTRY_AVALIABLE, itens.Count);
        }
    }
}
