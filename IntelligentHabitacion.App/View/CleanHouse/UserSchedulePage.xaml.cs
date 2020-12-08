using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.View.CleanHouse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSchedulePage : ContentPage
    {
        public UserSchedulePage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            ((ListView)sender).BackgroundColor = Color.Transparent;
        }
    }
}