using IntelligentHabitacion.App.Services;
using System.ComponentModel;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View.Friends
{
    [DesignTimeVisible(false)]
    public partial class FriendDetailsPage : ContentPage
    {
        public FriendDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ToolbarItems.Clear();
            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();
            if (userPreferences.IsAdministrator)
            {
                var itemToolbar = new ToolbarItem
                {
                    Text = "",
                    IconImageSource = ImageSource.FromFile("IconMenuDots"),
                    Priority = 0
                };

                itemToolbar.SetBinding(MenuItem.CommandProperty, new Binding("MenuOptionsCommand"));

                ToolbarItems.Add(itemToolbar);
            }

            base.OnAppearing();
        }
    }
}
