using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class MyFoodsRule : IMyFoodsRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly ISqliteDatabase _database;

        public MyFoodsRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, ISqliteDatabase database)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _database = database;
        }

        public async Task<string> AddItem(FoodModel model)
        {
            ValidateItem(model);

            var response = await _httpClient.AddMyFood(new Communication.Request.RequestAddMyFoodJson
            {
                Quantity = model.Quantity,
                DueDate = model.DueDate,
                Manufacturer = model.Manufacturer,
                Name = model.Name,
                Type = (Communication.Response.Type)model.Type
            }, _database.Get().Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _database.UpdateToken(response.Token);

            return response.Response.ToString();
        }

        public async Task DeleteMyFood(string id)
        {
            var response = await _httpClient.DeleteMyFood(id, _database.Get().Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _database.UpdateToken(response.Token);
        }

        public async Task<List<FoodModel>> GetMyFoods()
        {
            var response = await _httpClient.GetMyFoods(_database.Get().Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _database.UpdateToken(response.Token);

            var responseFoods = (List<ResponseMyFoodJson>)response.Response;

            return responseFoods.Select(c => new FoodModel
            {
                Id = c.Id,
                Quantity = c.Quantity,
                DueDate = c.DueDate,
                Manufacturer = c.Manufacturer,
                Name = c.Name,
                Type = (Model.Type)c.Type
            }).ToList();
        }

        public async Task ChangeQuantity(FoodModel model)
        {
            var response = await _httpClient.ChangeQuantityMyFood(new Communication.Request.RequestChangeQuantityMyFoodJson
            {
                Id = model.Id,
                Quantity = model.Quantity
            }, _database.Get().Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _database.UpdateToken(response.Token);
        }

        private void ValidateItem(FoodModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ProductNameEmptyException();

            if (model.Quantity <= 0)
                throw new QuantityProductsInvalidException();
        }

        public async Task EditItem(FoodModel model)
        {
            ValidateItem(model);

            var response = await _httpClient.EditMyFood(new Communication.Request.RequestEditMyFoodJson
            {
                Id = model.Id,
                Quantity = model.Quantity,
                DueDate = model.DueDate,
                Manufacturer = model.Manufacturer,
                Name = model.Name,
                Type = (Communication.Response.Type)model.Type
            }, _database.Get().Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _database.UpdateToken(response.Token);
        }
    }
}
