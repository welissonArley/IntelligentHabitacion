using IntelligentHabitacion.App.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntelligentHabitacion.App.ViewModel.MyFoods
{
    public class MyFoodsViewModel : BaseViewModel
    {
        public ICommand SearchTextChangedCommand { protected set; get; }
        public ICommand TappedChangeAmountCommand { protected set; get; }

        private ObservableCollection<FoodModel> _foodsList { get; set; }
        public ObservableCollection<FoodModel> FoodsList { get; set; }

        public bool FoodsListIsEmpty { get; set; }

        public MyFoodsViewModel()
        {
            _foodsList = new ObservableCollection<FoodModel>
            {
                new FoodModel
                {
                    Id = "1",
                    Amount = 5,
                    DueDate = DateTime.Today,
                    Manufacturer = "Coca-Cola",
                    Name = "Kuat Lata 350ml",
                    Type = Model.Type.Unity
                },
                new FoodModel
                {
                    Id = "2",
                    Amount = 1,
                    Manufacturer = "Sadia",
                    Name = "Frango desfiado",
                    Type = Model.Type.Package
                }
            };
            FoodsList = _foodsList;
            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });
            TappedChangeAmountCommand = new Command((value) =>
            {
                OnChangeAmount((FoodModel)value);
            });
        }

        private void OnSearchTextChanged(string value)
        {
            FoodsList = new ObservableCollection<FoodModel>(_foodsList.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            OnPropertyChanged(new PropertyChangedEventArgs("FoodsList"));
        }
        private void OnChangeAmount(FoodModel model)
        {
            if(model.Amount <= 0)
            {
                _foodsList.Remove(_foodsList.First(c => c.Id.Equals(model.Id)));
                if(FoodsList.Any(c => c.Id == model.Id))
                    FoodsList.Remove(FoodsList.First(c => c.Id.Equals(model.Id)));
            }

            OnPropertyChanged(new PropertyChangedEventArgs("FoodsList"));
        }
    }
}
