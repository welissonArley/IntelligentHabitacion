using IntelligentHabitacion.App.ViewModel.Friends.Add;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.View.Friends.Add
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcceptNewFriendPage : ContentPage
    {
        public AcceptNewFriendPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            var binding = (AcceptNewFriendViewModel)BindingContext;
            binding.DisconnectFromSocket();
            return base.OnBackButtonPressed();
        }
    }
}