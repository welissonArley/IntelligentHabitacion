using IntelligentHabitacion.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.View.CleanHouse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTasksPage : ContentPage
    {
        public MyTasksPage()
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
            else
                ButtonCreateSchedule.IsVisible = false;

            base.OnAppearing();
        }
    }
}