using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.View.Modal
{
    public enum Type
    {
        Green = 0,
        Red = 1
    }

    [DesignTimeVisible(false)]
    public partial class ConfirmAction : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly ICommand _callbackConfirm;
        private readonly ICommand _callbackCancel;

        public ConfirmAction(string title, string text, Type type, ICommand callbackConfirm, ICommand callbackCancel = null)
        {
            InitializeComponent();

            _callbackConfirm = callbackConfirm;
            _callbackCancel = callbackCancel;

            LabelTitle.Text = title;
            LabelText.Text = text;

            switch (type)
            {
                case Type.Green:
                    {
                        LabelTitle.TextColor = (Color)Application.Current.Resources["GreenDefault"];
                        ButtonOk.BackgroundColor = (Color)Application.Current.Resources["YellowDefault"];
                        ImageIcon.Source = ImageSource.FromFile("IconCheck");
                    }
                    break;
                case Type.Red:
                    {
                        LabelTitle.TextColor = (Color)Application.Current.Resources["RedDefault"];
                        ButtonOk.BackgroundColor = (Color)Application.Current.Resources["RedDefault"];
                    }
                    break;
            }
        }

        private async void Button_Cancel(object sender, EventArgs e)
        {
            _callbackCancel?.Execute(null);
            await Navigation.PopPopupAsync();
        }
        private async void Button_Ok(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
            _callbackConfirm.Execute(null);
        }
    }
}
