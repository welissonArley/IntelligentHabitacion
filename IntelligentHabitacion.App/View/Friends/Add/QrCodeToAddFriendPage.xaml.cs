using IntelligentHabitacion.App.ViewModel.Friends.Add;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntelligentHabitacion.App.View.Friends.Add
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrCodeToAddFriendPage : ContentPage
    {
        public QrCodeToAddFriendPage()
        {
            InitializeComponent();

            BarcodeView.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            BarcodeView.BarcodeOptions.Width = 170;
            BarcodeView.BarcodeOptions.Height = 170;
            BarcodeView.BarcodeOptions.Margin = 0;
        }

        protected override bool OnBackButtonPressed()
        {
            var binding = (QrCodeToAddFriendViewModel)BindingContext;
            binding.DisconnectFromSocket();
            return base.OnBackButtonPressed();
        }
    }
}