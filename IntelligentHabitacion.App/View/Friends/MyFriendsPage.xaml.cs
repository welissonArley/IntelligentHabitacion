using IntelligentHabitacion.App.Services;
using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View.Friends
{
    [DesignTimeVisible(false)]
    public partial class MyFriendsPage : ContentPage
    {
        public MyFriendsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();
            if (!userPreferences.IsAdministrator)
                ButtonAddFriend.IsVisible = false;

            base.OnAppearing();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            ((ListView)sender).BackgroundColor = Color.Transparent;
        }
    }
}
