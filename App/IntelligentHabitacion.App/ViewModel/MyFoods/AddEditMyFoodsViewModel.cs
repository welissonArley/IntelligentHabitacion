﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.UseCases.MyFoods.DeleteMyFood;
using IntelligentHabitacion.App.UseCases.MyFoods.RegisterMyFood;
using IntelligentHabitacion.App.UseCases.MyFoods.UpdateMyFood;
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

        private readonly Lazy<IRegisterMyFoodUseCase> registerUseCase;
        private readonly Lazy<IUpdateMyFoodUseCase> editUseCase;
        private readonly Lazy<IDeleteMyFoodUseCase> deleteUseCase;
        private IRegisterMyFoodUseCase _registerUseCase => registerUseCase.Value;
        private IUpdateMyFoodUseCase _editUseCase => editUseCase.Value;
        private IDeleteMyFoodUseCase _deleteUseCase => deleteUseCase.Value;

        public string Title { get; set; }
        public FoodModel Model { get; set; }

        public Action<FoodModel> CallbackSave { get; set; }
        public Action<FoodModel> CallbackDelete { get; set; }

        public AddEditMyFoodsViewModel(Lazy<IRegisterMyFoodUseCase> registerUseCase,
            Lazy<IUpdateMyFoodUseCase> editUseCase, Lazy<IDeleteMyFoodUseCase> deleteUseCase)
        {
            this.registerUseCase = registerUseCase;
            this.editUseCase = editUseCase;
            this.deleteUseCase = deleteUseCase;

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
                SendingData();

                FoodModel model = Model;

                if (string.IsNullOrEmpty(Model.Id))
                    model = await _registerUseCase.Execute(Model);
                else
                    await _editUseCase.Execute(Model);

                CallbackSave?.Invoke(model);

                await Sucess();

                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnSaveAndNew()
        {
            try
            {
                SendingData();

                var model = await _registerUseCase.Execute(Model);
                CallbackSave?.Invoke(model);

                Model = new FoodModel
                {
                    Quantity = 1.00m
                };
                OnPropertyChanged(new PropertyChangedEventArgs("Model"));

                await Sucess();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnDeleteItem()
        {
            try
            {
                SendingData();
                await _deleteUseCase.Execute(Model.Id);
                CallbackDelete(Model);
                await Sucess();
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