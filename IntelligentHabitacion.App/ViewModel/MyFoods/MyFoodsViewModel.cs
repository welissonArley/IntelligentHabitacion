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

        private readonly ObservableCollection<FoodModel> _foodsList;
        public ObservableCollection<FoodModel> FoodsList { get; set; }

        public bool FoodsListIsEmpty { get; set; }

        public MyFoodsViewModel()
        {
            FoodsList = new ObservableCollection<FoodModel>
            {
                new FoodModel
                {
                    Amount = 5,
                    DueDate = DateTime.Today,
                    Manufacturer = "Coca-Cola",
                    Name = "Kuat Lata 350ml",
                    Type = Model.Type.Unity
                },
                new FoodModel
                {
                    Amount = 1,
                    Manufacturer = "Sadia",
                    Name = "Frango desfiado",
                    Type = Model.Type.Package
                }
            };
            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });
        }

        private void OnSearchTextChanged(string value)
        {
            FoodsList = new ObservableCollection<FoodModel>(_foodsList.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
        }
    }
}
