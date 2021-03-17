using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Template.Informations;
using IntelligentHabitacion.App.UseCases.MyFoods.GetMyFoods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.MyFoods
{
    public class MyFoodsViewModel : BaseViewModel
    {
        private MyFoodsComponent componentToEdit { get; set; }

        private readonly Lazy<IGetMyFoodsUseCase> getMyFoodsUseCase;
        private IGetMyFoodsUseCase _getMyFoodsUseCase => getMyFoodsUseCase.Value;

        public ICommand SearchTextChangedCommand { protected set; get; }
        public ICommand TappedChangeQuantityCommand { protected set; get; }
        public ICommand AddNewItemCommand { protected set; get; }
        public ICommand TappedItemCommand { protected set; get; }

        private IList<FoodModel> _foodsList { get; set; }
        public ObservableCollection<FoodModel> FoodsList { get; set; }

        public MyFoodsViewModel(Lazy<IGetMyFoodsUseCase> getMyFoodsUseCase)
        {
            CurrentState = LayoutState.Loading;

            this.getMyFoodsUseCase = getMyFoodsUseCase;
            componentToEdit = null;

            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });
            TappedChangeQuantityCommand = new Command(async (value) =>
            {
                await OnChangeQuantity((FoodModel)value);
            });
            AddNewItemCommand = new Command(async() =>
            {
                await OnAddNewItem();
            });
            TappedItemCommand = new Command(async (component) =>
            {
                await OnEditItem((MyFoodsComponent)component);
            });
        }

        private void OnSearchTextChanged(string value)
        {
            FoodsList = new ObservableCollection<FoodModel>(_foodsList.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            OnPropertyChanged(new PropertyChangedEventArgs("FoodsList"));
        }
        private async Task OnChangeQuantity(FoodModel model)
        {
            try
            {
                /*await ShowLoading();
                await _myFoodsRule.ChangeQuantity(model);
                if (model.Quantity <= 0)
                {
                    _foodsList.Remove(_foodsList.First(c => c.Id.Equals(model.Id)));
                    FoodsList.Remove(FoodsList.First(c => c.Id.Equals(model.Id)));
                }
                HideLoading();*/
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task OnAddNewItem()
        {
            try
            {
                await ShowLoading();
                await Navigation.PushAsync<AddEditMyFoodsViewModel>((viewModel, page) =>
                {
                    viewModel.CallbackSave = NewItemAdded;
                    viewModel.Title = ResourceText.TITLE_NEW_ITEM;
                    viewModel.Model = new FoodModel { Quantity = 1.00m };
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }
        private async Task OnEditItem(MyFoodsComponent component)
        {
            try
            {
                await ShowLoading();
                componentToEdit = component;
                await Navigation.PushAsync<AddEditMyFoodsViewModel>((viewModel, page) =>
                {
                    viewModel.CallbackSave = EditItem;
                    viewModel.CallbackDelete = DeleteItem;
                    viewModel.Model = component.Food.Clone();
                    viewModel.Title = ResourceText.TITLE_EDIT;
                });
                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        private void EditItem(FoodModel model)
        {
            var item = _foodsList.First(c => c.Id.Equals(model.Id));
            FillModelEdit(item, model);
            componentToEdit.Refresh();
            componentToEdit = null;
        }
        private void NewItemAdded(FoodModel model)
        {
            _foodsList.Insert(0, model);
            FoodsList.Insert(0, model);
            OnPropertyChanged(new PropertyChangedEventArgs("FoodsList"));
        }
        private void DeleteItem(FoodModel model)
        {
            _foodsList.Remove(_foodsList.First(c => c.Id.Equals(model.Id)));
            FoodsList.Remove(FoodsList.First(c => c.Id.Equals(model.Id)));
            OnPropertyChanged(new PropertyChangedEventArgs("FoodsList"));
        }

        private void FillModelEdit(FoodModel copyTo, FoodModel from)
        {
            copyTo.Name = from.Name;
            copyTo.Manufacturer = from.Manufacturer;
            copyTo.Type = from.Type;
            copyTo.DueDate = from.DueDate;
            copyTo.Quantity = from.Quantity;
        }

        public async Task Initialize()
        {
            _foodsList = await _getMyFoodsUseCase.Execute();
            FoodsList = new ObservableCollection<FoodModel>(_foodsList);
            OnPropertyChanged(new PropertyChangedEventArgs("FoodsList"));
            CurrentState = _foodsList.Any() ? LayoutState.None : LayoutState.Empty;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
