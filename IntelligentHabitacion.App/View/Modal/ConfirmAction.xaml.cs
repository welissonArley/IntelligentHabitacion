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
        private readonly ICommand _action;

        public ConfirmAction(string title, string text, Type type, ICommand action)
        {
            InitializeComponent();

            _action = action;

            LabelTitle.Text = title;
            LabelText.Text = text;

            switch (type)
            {
                case Type.Green:
                    {
                        LabelTitle.TextColor = (Color)Application.Current.Resources["GreenDefault"];
                        ButtonOk.BackgroundColor = (Color)Application.Current.Resources["GreenDefault"];
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

        private void Button_Cancel(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
        private void Button_Ok(object sender, EventArgs e)
        {
            _action.Execute(null);
            Navigation.PopPopupAsync();
        }
    }
}
