﻿using Rg.Plugins.Popup.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.View.Modal.MenuOptions
{
    [DesignTimeVisible(false)]
    public partial class FloatActionUserIsPartOfHomeModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ICommand LoggoutCommand { get; }
        private ICommand AddNewItemCommand { get; }

        public FloatActionUserIsPartOfHomeModal(ICommand loggoutCommand, ICommand addNewItemCommand)
        {
            InitializeComponent();

            LoggoutCommand = loggoutCommand;
            AddNewItemCommand = addNewItemCommand;
        }

        private async Task CloseThisModal()
        {
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PopPopupAsync();
        }

        private async void Loggout_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            LoggoutCommand.Execute(null);
        }
        private async void AddNewItem_Tapped(object sender, System.EventArgs e)
        {
            await CloseThisModal();
            AddNewItemCommand.Execute(null);
        }
    }
}
