﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.Template.Informations;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.MyFoods
{
    public class MyFoodsViewModel : BaseViewModel
    {
        private MyFoodsComponent componentToEdit { get; set; }

        private readonly IMyFoodsRule _myFoodsRule;

        public ICommand SearchTextChangedCommand { protected set; get; }
        public ICommand TappedChangeAmountCommand { protected set; get; }
        public ICommand AddNewItemCommand { protected set; get; }
        public ICommand TappedItemCommand { protected set; get; }

        private ObservableCollection<FoodModel> _foodsList { get; set; }
        public ObservableCollection<FoodModel> FoodsList { get; set; }

        public bool FoodsListIsEmpty { get; set; }

        public MyFoodsViewModel(IMyFoodsRule myFoodsRule)
        {
            _myFoodsRule = myFoodsRule;
            componentToEdit = null;

            var foodsList = Task.Run(async () => await myFoodsRule.GetMyFoods()).Result;
            _foodsList = new ObservableCollection<FoodModel>(foodsList);
            FoodsList = new ObservableCollection<FoodModel>(foodsList);

            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });
            TappedChangeAmountCommand = new Command(async (value) =>
            {
                await OnChangeAmount((FoodModel)value);
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
        private async Task OnChangeAmount(FoodModel model)
        {
            try
            {
                await ShowLoading();
                await _myFoodsRule.ChangeQuantity(model);
                if (model.Amount <= 0)
                {
                    _foodsList.Remove(_foodsList.First(c => c.Id.Equals(model.Id)));
                    FoodsList.Remove(FoodsList.First(c => c.Id.Equals(model.Id)));
                }
                HideLoading();
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
                    viewModel.Model = new FoodModel { Amount = 1 };
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
            copyTo.Amount = from.Amount;
        }
    }
}
