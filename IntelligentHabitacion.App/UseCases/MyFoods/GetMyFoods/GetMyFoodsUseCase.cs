﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.MyFoods;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.MyFoods.GetMyFoods
{
    public class GetMyFoodsUseCase : UseCaseBase, IGetMyFoodsUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IMyFoodsService _restService;

        public GetMyFoodsUseCase(Lazy<UserPreferences> userPreferences) : base("MyFood")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IMyFoodsService>(BaseAddress());
        }

        public async Task<IList<FoodModel>> Execute()
        {
            var response = await _restService.GetMyFoods(await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        public IList<FoodModel> Mapper(List<ResponseMyFoodJson> myFoodJsons)
        {
            return myFoodJsons.Select(c => new FoodModel
            {
                Id = c.Id,
                Quantity = c.Quantity,
                DueDate = c.DueDate,
                Manufacturer = c.Manufacturer,
                Name = c.Name,
                Type = (Model.Type)c.Type
            }).ToList();
        }
    }
}
