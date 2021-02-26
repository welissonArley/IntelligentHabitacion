using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View.Modal
{
    [DesignTimeVisible(false)]
    public partial class ChoosePhonenumberModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly Func<string, Task> _callbackPhonenumberSelected;

        public ChoosePhonenumberModal(string name, string phonenumber1, string phonenumber2, string color, Func<string, Task> callbackPhonenumberSelected)
        {
            InitializeComponent();

            _callbackPhonenumberSelected = callbackPhonenumberSelected;
            ShortName.Text = new Useful.ShortNameConverter().Converter(name);
            BackgroundShortName.BackgroundColor = Color.FromHex(color);
            BackgroundCallTo.BackgroundColor = Color.FromHex(color);
            LabelBackgroundCallTo.Text = string.Format(ResourceText.TITLE_CALL_TO_TWOPOINTS, name);
            NumbersList.ItemsSource = new ObservableCollection<NumbersContact>
            {
                new NumbersContact
                {
                    TitleNumber = ResourceText.TITLE_PHONENUMBER_1_TWOPOINTS,
                    Number = phonenumber1
                },
                new NumbersContact
                {
                    TitleNumber = ResourceText.TITLE_PHONENUMBER_2_TWOPOINTS,
                    Number = phonenumber2
                }
            };
        }

        private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NumbersContact current = (e.CurrentSelection.FirstOrDefault() as NumbersContact);
            Navigation.PopPopupAsync();
            _callbackPhonenumberSelected(current.Number);
        }
    }

    public class NumbersContact
    {
        public string TitleNumber { get; set; }
        public string Number { get; set; }
    }
}
