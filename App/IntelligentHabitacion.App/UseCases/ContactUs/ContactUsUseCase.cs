﻿using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.Services.Communication.ContactUs;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using Refit;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.UseCases.ContactUs
{
    public class ContactUsUseCase : UseCaseBase, IContactUsUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IContactUsService _restService;

        public ContactUsUseCase(Lazy<UserPreferences> userPreferences) : base("ContactUs")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IContactUsService>(BaseAddress());
        }

        public async Task Execute(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new MessageEmptyException();

            var response = await _restService.SendMessage(new RequestContactUsJson
            {
                Message = message
            }, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }
    }
}
