using IntelligentHabitacion.Communication.Boolean;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Communication.Url;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ErrorJson;
using IntelligentHabitacion.Exception.ExceptionsBase;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Communication
{
    public class IntelligentHabitacionHttpClient : HttpClient, IIntelligentHabitacionHttpClient
    {
        private readonly string UrlIntelligentHabitacionApi;

        public IntelligentHabitacionHttpClient()
        {
            UrlIntelligentHabitacionApi = $"https://{UrlHelper.IntelligentHabitacionApi}/api/v1";
        }

        private async Task<HttpResponseMessage> SendRequisition(HttpMethod httpMethod, string uri, object content = null, string token = null, string language = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, uri);

            if (content != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", token);

            if (!string.IsNullOrWhiteSpace(language))
                request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
            else
                request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("pt-BR"));

            var resposta = await SendAsync(request);
            await ResponseValidate(resposta);
            return resposta;
        }
        private async Task ResponseValidate(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var token = GetToken(responseMessage);
                var errorJson = JsonConvert.DeserializeObject<ErrorJson>(await responseMessage.Content.ReadAsStringAsync());
                switch (responseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                        {
                            throw new ResponseException
                            {
                                Token = token,
                                Exception = new ErrorOnValidationException(errorJson.Errors)
                            };
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        {
                            throw new ResponseException
                            {
                                Token = token,
                                Exception = new NotFoundException(errorJson.Errors[0])
                            };
                        }
                    case System.Net.HttpStatusCode.Unauthorized:
                        {
                            if(errorJson.ErrorCode == ErrorCode.TokenExpired)
                                throw new TokenExpiredException();
                            
                            throw new ResponseException
                            {
                                Token = token,
                                Exception = new IntelligentHabitacionException(errorJson.Errors[0])
                            };
                        }
                    default:
                        {
                            throw new ResponseException
                            {
                                Token = token,
                                Exception = new IntelligentHabitacionException(ResourceTextException.UNKNOW_ERROR)
                            };
                        }
                }
            }
        }
        private string GetToken(HttpResponseMessage responseMessage)
        {
            return responseMessage.Headers.Contains("Tvih") ? responseMessage.Headers.GetValues("Tvih")?.First() : null;
        }

        public async Task<ResponseLocationBrazilJson> GetLocationBrazilByZipCode(string zipcode)
        {
            var response = await SendRequisition(HttpMethod.Get, $"https://viacep.com.br/ws/{zipcode.Replace(".", "").Replace("-","")}/json/");

            var errorJson = (JsonConvert.DeserializeObject<ErrorTrueJson>(await response.Content.ReadAsStringAsync()));
            //if (errorJson.Erro)
                //throw new ZipCodeInvalidException();

            return JsonConvert.DeserializeObject<ResponseLocationBrazilJson>(await response.Content.ReadAsStringAsync());
        }

        #region User
        public async Task<ResponseJson> CreateUser(RequestRegisterUserJson registerUser, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/User/Register", registerUser, language: language);

            return new ResponseJson
            {
                Response = JsonConvert.DeserializeObject<ResponseUserRegisteredJson>(await response.Content.ReadAsStringAsync()),
                Token = GetToken(response)
            };
        }
        public async Task<BooleanJson> EmailAlreadyBeenRegistered(string email, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/User/EmailAlreadyBeenRegistered/{email}", language: language);

            return JsonConvert.DeserializeObject<BooleanJson>(await response.Content.ReadAsStringAsync());
        }
        public async Task<ResponseJson> GetUsersInformations(string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/User/Informations", token: token, language: language);
            return new ResponseJson
            {
                Response = JsonConvert.DeserializeObject<ResponseUserInformationsJson>(await response.Content.ReadAsStringAsync()),
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> UpdateUsersInformations(RequestUpdateUserJson updateUser, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Put, $"{UrlIntelligentHabitacionApi}/User/Update", updateUser, token: token, language: language);
            return new ResponseJson
            {
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> ChangePassword(RequestChangePasswordJson changePassword, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Put, $"{UrlIntelligentHabitacionApi}/User/ChangePassword", changePassword, token: token, language: language);
            return new ResponseJson
            {
                Token = GetToken(response)
            };
        }
        #endregion

        #region Login
        public async Task RequestCodeResetPassword(string email, string language = null)
        {
            await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/Login/RequestCodeResetPassword/{email}", language: language);
        }
        public async Task<ResponseJson> Login(RequestLoginJson loginUser, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/Login", loginUser, language: language);
            return new ResponseJson
            {
                Response = JsonConvert.DeserializeObject<ResponseLoginJson>(await response.Content.ReadAsStringAsync()),
                Token = GetToken(response)
            };
        }
        public async Task ChangePasswordForgotPassword(RequestResetYourPasswordJson resetYourPassword, string language = null)
        {
            await SendRequisition(HttpMethod.Put, $"{UrlIntelligentHabitacionApi}/Login/ResetYourPassword", resetYourPassword, language: language);
        }
        #endregion

        #region Home
        public async Task<ResponseJson> CreateHome(RequestRegisterHomeJson registerHome, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/Home/Register", registerHome, token: token, language: language);
            return new ResponseJson
            {
                Response = null,
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> GetHomesInformations(string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/Home/Informations", token: token, language: language);
            return new ResponseJson
            {
                Response = JsonConvert.DeserializeObject<ResponseHomeInformationsJson>(await response.Content.ReadAsStringAsync()),
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> UpdateHome(RequestUpdateHomeJson registerHome, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Put, $"{UrlIntelligentHabitacionApi}/Home/Update", registerHome, token: token, language: language);
            return new ResponseJson
            {
                Response = null,
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> DeleteHome(RequestAdminActionJson request, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Delete, $"{UrlIntelligentHabitacionApi}/Home/Delete", request, token: token, language: language);
            return new ResponseJson
            {
                Response = null,
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> RequestCodeToDeleteHome(string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/Home/RequestCodeDeleteHome", token: token, language: language);
            return new ResponseJson
            {
                Response = null,
                Token = GetToken(response)
            };
        }
        #endregion

        #region Friends
        public async Task<ResponseJson> GetHouseFriends(string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/Friend/Friends", token: token, language: language);
            return new ResponseJson
            {
                Response = response.StatusCode == System.Net.HttpStatusCode.NoContent ? new List<ResponseFriendJson>() : JsonConvert.DeserializeObject<List<ResponseFriendJson>>(await response.Content.ReadAsStringAsync()),
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> ChangeDateJoinHome(string friendId, RequestDateJson request, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Put, $"{UrlIntelligentHabitacionApi}/Friend/ChangeDateJoinHome/{friendId}", request, token: token, language: language);
            return new ResponseJson
            {
                Response = JsonConvert.DeserializeObject<ResponseFriendJson>(await response.Content.ReadAsStringAsync()),
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> NotifyFriendOrderHasArrived(string friendId, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/Friend/NotifyOrderReceived/{friendId}", token: token, language: language);
            return new ResponseJson
            {
                Response = null,
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> RequestCodeToChangeAdministrator(string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/Friend/RequestCodeChangeAdministrator", token: token, language: language);
            return new ResponseJson
            {
                Response = null,
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> ChangeAdministrator(string friendId, RequestAdminActionJson request, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/Friend/ChangeAdministrator/{friendId}", request, token: token, language: language);
            return new ResponseJson
            {
                Response = null,
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> RequestCodeToRemoveFriend(string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/Friend/RequestCodeRemoveFriend", token: token, language: language);
            return new ResponseJson
            {
                Response = null,
                Token = GetToken(response)
            };
        }
        public async Task<ResponseJson> RemoveFriend(string friendId, RequestAdminActionJson request, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/Friend/RemoveFriend/{friendId}", request, token: token, language: language);
            return new ResponseJson
            {
                Response = null,
                Token = GetToken(response)
            };
        }
        #endregion

        #region MyFood
        public async Task<ResponseJson> AddMyFood(RequestProductJson myFood, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/MyFood/AddFood", myFood, token: token, language: language);
            return new ResponseJson
            {
                Response = (await response.Content.ReadAsStringAsync()).ToString(),
                Token = GetToken(response)
            };
        }

        public async Task<ResponseJson> EditMyFood(string myFoodId, RequestProductJson myFood, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Put, $"{UrlIntelligentHabitacionApi}/MyFood/EditFood/{myFoodId}", myFood, token: token, language: language);
            return new ResponseJson
            {
                Token = GetToken(response)
            };
        }

        public async Task<ResponseJson> DeleteMyFood(string id, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Delete, $"{UrlIntelligentHabitacionApi}/MyFood/Delete/{id}", token: token, language: language);
            return new ResponseJson
            {
                Token = GetToken(response)
            };
        }

        public async Task<ResponseJson> ChangeQuantityMyFood(string myFoodId, decimal amount, string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Put, $"{UrlIntelligentHabitacionApi}/MyFood/ChangeQuantity/{myFoodId}", new RequestChangeQuantityMyFoodJson { Amount = amount}, token: token, language: language);
            return new ResponseJson
            {
                Token = GetToken(response)
            };
        }

        public async Task<ResponseJson> GetMyFoods(string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/MyFood/MyFoods/", token: token, language: language);
            return new ResponseJson
            {
                Response = response.StatusCode == System.Net.HttpStatusCode.NoContent ? new List<ResponseMyFoodJson>() : JsonConvert.DeserializeObject<List<ResponseMyFoodJson>>(await response.Content.ReadAsStringAsync()),
                Token = GetToken(response)
            };
        }

        #endregion

        #region CleaningSchedule

        public async Task<ResponseJson> GetMyTasksCleaningSchedule(string token, RequestDateJson date, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/CleaningSchedule/MyTasks/", date, token: token, language: language);
            var responseJson = new ResponseJson
            {
                Token = GetToken(response)
            };

            if (response.StatusCode == System.Net.HttpStatusCode.PartialContent)
                responseJson.Response = JsonConvert.DeserializeObject<ResponseNeedActionJson>(await response.Content.ReadAsStringAsync());
            else
                responseJson.Response = JsonConvert.DeserializeObject<ResponseMyTasksCleaningScheduleJson>(await response.Content.ReadAsStringAsync());

            return responseJson;
        }

        public async Task<ResponseJson> GetCleaningSchedule(string token, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/CleaningSchedule/CleaningSchedule/", token: token, language: language);
            var responseJson = new ResponseJson
            {
                Token = GetToken(response),
                Response = JsonConvert.DeserializeObject<ResponseManageScheduleJson>(await response.Content.ReadAsStringAsync())
            };

            return responseJson;
        }

        public async Task<ResponseJson> UpdateCleaningSchedule(string token, IList<RequestUpdateCleaningScheduleJson> request, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/CleaningSchedule/CleaningSchedule/", request, token: token, language: language);
            var responseJson = new ResponseJson
            {
                Token = GetToken(response)
            };

            return responseJson;
        }

        public async Task<ResponseJson> TaskCompletedToday(string token, string id, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Put, $"{UrlIntelligentHabitacionApi}/CleaningSchedule/TaskCompleted/{id}", token: token, language: language);
            var responseJson = new ResponseJson
            {
                Token = GetToken(response)
            };

            return responseJson;
        }

        public async Task<ResponseJson> GetUsersTaskDetails(string token, string id, RequestDateJson date, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/CleaningSchedule/UserTaskDetails/{id}", date, token: token, language: language);
            var responseJson = new ResponseJson
            {
                Token = GetToken(response),
                Response = JsonConvert.DeserializeObject<ResponseDetailsUserScheduleJson>(await response.Content.ReadAsStringAsync())
            };

            return responseJson;
        }

        public async Task<ResponseJson> GetFriendsTasks(string token, RequestDateJson date, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Post, $"{UrlIntelligentHabitacionApi}/CleaningSchedule/FriendsTasks", date, token: token, language: language);
            var responseJson = new ResponseJson
            {
                Token = GetToken(response),
                Response = JsonConvert.DeserializeObject<List<ResponseAllFriendsTasksScheduleJson>>(await response.Content.ReadAsStringAsync())
            };

            return responseJson;
        }

        public async Task<ResponseJson> RateFriendTask(string token, string taskId, RequestRateTaskJson request, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Put, $"{UrlIntelligentHabitacionApi}/CleaningSchedule/RateTask/{taskId}", request, token: token, language: language);
            var responseJson = new ResponseJson
            {
                Token = GetToken(response),
                Response = JsonConvert.DeserializeObject<ResponseAverageRatingJson>(await response.Content.ReadAsStringAsync())
            };

            return responseJson;
        }

        public async Task<ResponseJson> GetRatesTask(string token, string taskId, string language = null)
        {
            var response = await SendRequisition(HttpMethod.Get, $"{UrlIntelligentHabitacionApi}/CleaningSchedule/Feedbacks/{taskId}", token: token, language: language);
            var responseJson = new ResponseJson
            {
                Token = GetToken(response),
                Response = JsonConvert.DeserializeObject<ResponseRateTaskJson>(await response.Content.ReadAsStringAsync())
            };

            return responseJson;
        }

        #endregion
    }
}