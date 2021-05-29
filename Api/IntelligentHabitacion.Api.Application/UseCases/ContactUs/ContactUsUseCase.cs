using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Api.Domain.ValueObjects;
using IntelligentHabitacion.Communication.Request;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ContactUs
{
    public class ContactUsUseCase : IContactUsUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ISendEmail _emailHelper;

        public ContactUsUseCase(ISendEmail emailHelper, ILoggedUser loggedUser,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase)
        {
            _emailHelper = emailHelper;
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
        }

        public async Task<ResponseOutput> Execute(RequestContactUsJson request)
        {
            var loggedUser = await _loggedUser.User();

            if (!string.IsNullOrWhiteSpace(request.Message))
            {
                await _emailHelper.SendMessageSupport(new EmailContent
                {
                    Subject = "Support Message",
                    HtmlText = BodyHtmlText(loggedUser.Name, loggedUser.Email, request.Message),
                    PlainText = BodyPlainText(loggedUser.Name, loggedUser.Email, request.Message)
                });
            }

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            return response;
        }

        private string BodyPlainText(string userName, string email, string mensagem)
        {
            var plainText = $"O usuário {userName} com o e-mail ({email}) enviou a seguinte mensagem:\n\n\n";
            plainText = $"{plainText}{mensagem}\n\n\n";

            return plainText;
        }
        private string BodyHtmlText(string userName, string email, string mensagem)
        {
            var htmlText = $@"<div style=""margin-top: 50px;"">
			    <span style=""font-family: 'Raleway';font-size: 14px;"">O Usuário {userName}, com e-mail ({email}) enviou a seguinte mensagem:</span>
			    
			    <div style=""margin-top: 50px;"">
				    <span style=""font-family: 'Raleway';font-size: 16px;font-weight: 800;"">{mensagem}</span>
			    </div>
		    </div>";

            return htmlText;
        }
    }
}
