using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.Template.Search
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchByNameComponent : ContentView
    {
        public ICommand EntryTextChanged
        {
            get { return (ICommand)GetValue(EntryTextChangedProperty); }
            set
            {
                SetValue(EntryTextChangedProperty, value);
            }
        }

        public static readonly BindableProperty EntryTextChangedProperty = BindableProperty.Create("EntryTextChanged", typeof(ICommand), typeof(SearchByNameComponent), defaultValue: null);


        public SearchByNameComponent()
        {
            InitializeComponent();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            EntryTextChanged?.Execute(entry.Text);
        }
    }
}