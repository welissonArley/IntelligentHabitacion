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
        #region CheckBox
        public bool IsCheckedUnity
        { 
            set { if (value) Model.Type = IntelligentHabitacion.App.Model.Type.Unity;}
            get { return Model.Type == IntelligentHabitacion.App.Model.Type.Unity; }
        }
        public bool IsCheckedBox
        {
            set { if(value) Model.Type = IntelligentHabitacion.App.Model.Type.Box; }
            get { return Model.Type == IntelligentHabitacion.App.Model.Type.Box; }
        }
        public bool IsCheckedPackage
        {
            set { if (value) Model.Type = IntelligentHabitacion.App.Model.Type.Package; }
            get { return Model.Type == IntelligentHabitacion.App.Model.Type.Package; }
        }
        public bool IsCheckedKilogram
        {
            set { if (value) Model.Type = IntelligentHabitacion.App.Model.Type.Kilogram; }
            get { return Model.Type == IntelligentHabitacion.App.Model.Type.Kilogram; }
        }
        #endregion

        public ICommand SelectDueDateTapped { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveAndNewCommand { get; }

        private readonly IMyFoodsRule _myFoodsRule;

        public string Title { get; set; }
        public FoodModel Model { get; set; }

        public Action<FoodModel> CallbackSave { get; set; }
        public Action<FoodModel> CallbackDelete { get; set; }

        public AddEditMyFoodsViewModel(IMyFoodsRule myFoodsRule)
        {
            _myFoodsRule = myFoodsRule;
            
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
            DeleteCommand = new Command(async () =>
            {
                await OnDeleteItem();
            });
        }

        private async Task OnSaveItem()
        {
            try
            {
                await ShowLoading();

                if (string.IsNullOrEmpty(Model.Id))
                    Model.Id = await _myFoodsRule.AddItem(Model);
                else
                    await _myFoodsRule.EditItem(Model);

                CallbackSave?.Invoke(Model);

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
                Model.Id = await _myFoodsRule.AddItem(Model);
                CallbackSave?.Invoke(Model);
                Model.Id = string.Empty;
                Model.Name = "";
                Model.Manufacturer = "";
                Model.Quantity = 1.00m;
                Model.DueDate = null;
                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task OnDeleteItem()
        {
            try
            {
                await ShowLoading();
                await _myFoodsRule.DeleteMyFood(Model.Id);
                CallbackDelete(Model);
                HideLoading();
                await Navigation.PopAsync();
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
            await navigation.PushPopupAsync(new Calendar(Model.DueDate ?? DateTime.Today, OnDateSelected, minimumDate: DateTime.Today));
            HideLoading();
        }
        private Task OnDateSelected(DateTime date)
        {
            Model.DueDate = date;
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            return Task.CompletedTask;
        }
    }
}
