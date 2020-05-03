using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Exception;
using System;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class MyFoodsRule : IMyFoodsRule
    {
        public string AddItem(FoodModel model)
        {
            ValidateItem(model);

            return DateTime.Now.ToString("MMddyyHHmmss");
        }

        private void ValidateItem(FoodModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ProductNameEmptyException();

            if (model.Amount <= 0)
                throw new AmountProductsInvalidException();
        }
    }
}
