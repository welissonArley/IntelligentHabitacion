using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.SetOfRules.Interface
{
    public interface IMyFoodRule
    {
        List<ResponseMyFoodJson> GetMyFoods();
        string Create(RequestProductJson requestMyFood);
        void Delete(string encryptedId);
        void ChangeQuantity(decimal changeQuantity);
        void Edit(RequestProductJson editMyFood);
    }
}
