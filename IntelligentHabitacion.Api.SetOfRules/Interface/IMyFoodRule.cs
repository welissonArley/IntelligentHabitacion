using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IMyFoodRule
    {
        List<ResponseMyFoodJson> GetMyFoods();
        string Create(RequestAddMyFoodJson requestMyFood);
        void Delete(string encryptedId);
        void ChangeQuantity(RequestChangeQuantityMyFoodJson changeQuantity);
        void Edit(RequestEditMyFoodJson editMyFood);
    }
}
