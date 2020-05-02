using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.View.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace IntelligentHabitacion.App.ViewModel.MyFoods
{
    public class AddEditMyFoodsViewModel : BaseViewModel
    {
        public ICommand SelectDueDateTapped { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAndNewCommand { get; }

        private readonly IMyFoodsRule _myFoodsRule;

        public string Title { get; set; }
        public FoodModel Model { get; set; }

        public Action<FoodModel> CallbackSave { get; set; }

        public AddEditMyFoodsViewModel(IMyFoodsRule myFoodsRule)
        {
            _myFoodsRule = myFoodsRule;
            if (Model == null)
            {
                Title = ResourceText.TITLE_NEW_ITEM;
                Model = new FoodModel();
            }
            else
                Title = ResourceText.TITLE_EDIT;

            SelectDueDateTapped = new Command(async() =>
            {
                await ClickSelectDueDate();
            });
            SaveCommand = new Command(async() =>
            {
                await OnSaveItem();
            });
            SaveAndNewCommand = new Command(async() =>
            {
                await OnSaveAndNew();
            });
        }

        private async Task OnSaveItem()
        {
            try
            {
                await ShowLoading();
                Model.Id = _myFoodsRule.AddItem(Model);
                CallbackSave(Model);
                HideLoading();
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task OnSaveAndNew()
        {
            try
            {
                await ShowLoading();
                Model.Id = _myFoodsRule.AddItem(Model);
                CallbackSave(Model);
                Model.Name = "";
                Model.Manufacturer = "";
                Model.Amount = 0;
                Model.DueDate = null;
                Model.Type = IntelligentHabitacion.App.Model.Type.Unity;
                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task ClickSelectDueDate()
        {
            await ShowLoading();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new Calendar(DateTime.Today, OnDateSelected, minimumDate: DateTime.Today));
            HideLoading();
        }
        private void OnDateSelected(DateTime date)
        {
            Model.DueDate = date;
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
        }
    }
}
