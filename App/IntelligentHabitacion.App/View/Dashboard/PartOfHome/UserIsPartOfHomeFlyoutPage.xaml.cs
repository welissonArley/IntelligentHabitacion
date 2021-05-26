using IntelligentHabitacion.App.ViewModel.Dashboard.PartOfHome;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Mvvm;

namespace IntelligentHabitacion.App.View.Dashboard.PartOfHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserIsPartOfHomeFlyoutPage : FlyoutPage
    {
        public UserIsPartOfHomeFlyoutPage()
        {
            InitializeComponent();

            Flyout = (Page)ViewFactory.CreatePage<UserIsPartOfHomeFlyoutViewModel, UserIsPartOfHomeFlyoutPageFlyout>();

            Detail.BindingContext = new UserIsPartOfHomeDetailViewModel(Navigation);
        }        
    }
}