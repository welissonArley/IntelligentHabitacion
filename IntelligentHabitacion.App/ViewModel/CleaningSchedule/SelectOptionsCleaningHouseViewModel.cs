using IntelligentHabitacion.App.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.CleaningSchedule
{
    public class SelectOptionsCleaningHouseViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Phrase { get; set; }
        public string SubTitle { get; set; }
        public ICommand ConcludeCommand { get; }
        public ICommand CallbackOnConclude { set; get; }
        public ObservableCollection<SelectOptionModel> Options { get; set; }

        public SelectOptionsCleaningHouseViewModel()
        {
            ConcludeCommand = new Command(async () =>
            {
                CallbackOnConclude.Execute(Options.Where(c => c.Assigned).ToList());
                await Navigation.PopAsync();
            });
        }

        public void Initialize(string title, string phrase, string subTitle, ICommand callbackOnConclude, IList<SelectOptionModel> options)
        {
            Title = title;
            Phrase = phrase;
            SubTitle = subTitle;
            CallbackOnConclude = callbackOnConclude;
            Options = new ObservableCollection<SelectOptionModel>(options);

            OnPropertyChanged(new PropertyChangedEventArgs("Title"));
            OnPropertyChanged(new PropertyChangedEventArgs("Phrase"));
            OnPropertyChanged(new PropertyChangedEventArgs("SubTitle"));
            OnPropertyChanged(new PropertyChangedEventArgs("CallbackOnConclude"));
            OnPropertyChanged(new PropertyChangedEventArgs("Options"));
        }
    }
}
