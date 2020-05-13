using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.View.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.Friends.Add
{
    public class AcceptNewFriendViewModel : BaseViewModel
    {
        public AcceptNewFriendModel Model { get; set; }
        public string Time { get; set; }

        public ICommand SelectEntryDateTapped { get; }

        public AcceptNewFriendViewModel()
        {
            Model = new AcceptNewFriendModel
            {
                Name = "Anilton Barbosa",
                ProfileColor = "#000000",
                EntryDate = DateTime.Today,
                RentAmount = 650
            };
            Time = "00:59";

            SelectEntryDateTapped = new Command(async () =>
            {
                await ClickSelectDueDate();
            });
        }

        private async Task ClickSelectDueDate()
        {
            await ShowLoading();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new Calendar(Model.EntryDate, OnDateSelected, maximumDate: DateTime.Today));
            HideLoading();
        }
        private void OnDateSelected(DateTime date)
        {
            Model.EntryDate = date;
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
        }
    }
}
