﻿using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Refit;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.Services.Communication.User
{
    [Headers("Content-Type: application/json")]
    public interface IUserRestService
    {
        [Get("/EmailAlreadyBeenRegistered/{email}")]
        Task<BooleanJson> EmailAlreadyBeenRegistered(string email, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Post("/Register")]
        Task<ApiResponse<ResponseUserRegisteredJson>> CreateUser([Body] RequestRegisterUserJson registerUser, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Get("/Informations")]
        Task<ApiResponse<ResponseUserInformationsJson>> GetUserInformations([Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/Update")]
        Task<ApiResponse<string>> UpdateUser([Body] RequestUpdateUserJson updateUser, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/ChangePassword")]
        Task<ApiResponse<string>> ChangePassword([Body] RequestChangePasswordJson changePassword, [Authorize("Basic")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}