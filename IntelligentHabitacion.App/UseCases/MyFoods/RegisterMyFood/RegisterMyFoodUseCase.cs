﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.MyFoods;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using Refit;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.MyFoods.RegisterMyFood
{
    public class RegisterMyFoodUseCase : UseCaseBase, IRegisterMyFoodUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IMyFoodsService _restService;

        public RegisterMyFoodUseCase(Lazy<UserPreferences> userPreferences) : base("MyFood")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IMyFoodsService>(BaseAddress());
        }

        public async Task<FoodModel> Execute(FoodModel model)
        {
            ValidateItem(model);

            var json = Mapper(model);

            var response = await _restService.AddMyFood(json, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content, model);
        }

        private void ValidateItem(FoodModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                throw new ProductNameEmptyException();

            if (model.Quantity <= 0)
                throw new QuantityProductsInvalidException();
        }

        private RequestProductJson Mapper(FoodModel model)
        {
            return new RequestProductJson
            {
                Quantity = model.Quantity,
                DueDate = model.DueDate,
                Manufacturer = model.Manufacturer,
                Name = model.Name,
                Type = (Communication.Response.Type)model.Type
            };
        }

        private FoodModel Mapper(string idResponse, FoodModel model)
        {
            return new FoodModel
            {
                Id = idResponse,
                Quantity = model.Quantity,
                DueDate = model.DueDate,
                Manufacturer = model.Manufacturer,
                Name = model.Name,
                Type = model.Type
            };
        }
    }
}
