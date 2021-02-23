﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.Services;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.Useful;
using IntelligentHabitacion.Communication;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public abstract class HomeRule : IHomeRule
    {
        private readonly IIntelligentHabitacionHttpClient _httpClient;
        private readonly UserPreferences _userPreferences;

        public HomeRule(IIntelligentHabitacionHttpClient intelligentHabitacionHttpClient, UserPreferences userPreferences)
        {
            _httpClient = intelligentHabitacionHttpClient;
            _userPreferences = userPreferences;
        }

        public abstract Task Create(HomeModel model);
        public abstract Task UpdateInformations(HomeModel model);

        public async Task Delete(string code, string password)
        {
            var response = await _httpClient.DeleteHome(new RequestAdminActionJson
            {
                Code = code,
                Password = password
            }, _userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task RequestCodeDeleteHome()
        {
            var response = await _httpClient.RequestCodeToDeleteHome(_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);
        }

        public async Task<HomeModel> GetInformations()
        {
            var response = await _httpClient.GetHomesInformations(_userPreferences.Token, System.Globalization.CultureInfo.CurrentCulture.ToString());

            _userPreferences.ChangeToken(response.Token);

            var homeInformations = (ResponseHomeInformationsJson)response.Response;

            return new HomeModel
            {
                Address = homeInformations.Address,
                AdditionalAddressInfo = homeInformations.AdditionalAddressInfo,
                Neighborhood = homeInformations.Neighborhood,
                Number = homeInformations.Number,
                ZipCode = homeInformations.ZipCode,
                Rooms = new System.Collections.ObjectModel.ObservableCollection<RoomModel>
                (
                    homeInformations.Rooms.Select(c => new RoomModel
                    {
                        Id = c.Id, Room = c.Name
                    }).ToList()
                ),
                NetWork = new WifiNetworkModel
                {
                    Name = homeInformations.NetWork.Name,
                    Password = homeInformations.NetWork.Password
                },
                City = new CityModel
                {
                    Name = homeInformations.City,
                    StateProvinceName = homeInformations.City,
                    Country = new CoutriesAvaliables().Get().First(c => c.Id == (CountryEnum)homeInformations.Country)
                }
            };
        }
    }
}
