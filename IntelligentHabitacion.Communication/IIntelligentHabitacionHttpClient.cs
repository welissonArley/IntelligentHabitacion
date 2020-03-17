﻿using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Communication
{
    public interface IIntelligentHabitacionHttpClient
    {
        Task<BooleanJson> EmailAlreadyBeenRegistered(string email, string language = null);
        Task<ResponseJson> CreateUser(RequestRegisterUserJson registerUser, string language = null);
        Task<ResponseLocationBrazilJson> GetLocationBrazilByZipCode(string zipcode);
        Task<ResponseJson> Login(RequestLoginJson loginUser, string language = null);
    }
}