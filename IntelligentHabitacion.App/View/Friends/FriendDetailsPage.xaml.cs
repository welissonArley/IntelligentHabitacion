using IntelligentHabitacion.App.SQLite.Interface;
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
            var database = XLabs.Ioc.Resolver.Resolve<ISqliteDatabase>();
            if (database.Get().IsAdministrator)
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
