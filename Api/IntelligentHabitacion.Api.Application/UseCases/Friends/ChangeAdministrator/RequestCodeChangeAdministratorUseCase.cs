using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Api.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Friends.ChangeAdministrator
{
    public class RequestCodeChangeAdministratorUseCase : IRequestCodeChangeAdministratorUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICodeWriteOnlyRepository _repository;
        private readonly ISendEmail _emailHelper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestCodeChangeAdministratorUseCase(ILoggedUser loggedUser, ICodeWriteOnlyRepository repository,
            ISendEmail emailHelper, IUnitOfWork unitOfWork, IntelligentHabitacionUseCase intelligentHabitacionUseCase)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _emailHelper = emailHelper;
            _unitOfWork = unitOfWork;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
        }

        public async Task<ResponseOutput> Execute()
        {
            var loggedUser = await _loggedUser.User();

            var codeRandom = new CodeGenerator().Random6Chars();

            await _repository.Add(new Code
            {
                Type = Domain.Enums.CodeType.ChangeAdministrator,
                Value = codeRandom,
                UserId = loggedUser.Id
            });

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            await _emailHelper.Send(new EmailContent
            {
                SendToEmail = loggedUser.Email,
                Subject = "Definir novo Administrador",
                HtmlText = BodyHtmlText(loggedUser.Name, codeRandom),
                PlainText = BodyPlainText(loggedUser.Name, codeRandom)
            });

            return response;
        }

        private string BodyPlainText(string adminName, string code)
        {
            var plainText = $"Olá {adminName}, Use o código abaixo para definir seu amigo como o novo Administrador:\n\n\n";
            plainText = $"{plainText}{code}\n\n\n";
            plainText = $"{plainText}Mas lembre-se, não deixe pra depois pois este código será valido por apenas 10 minutos, combinado?\n\n\n";
            plainText = $"{plainText}Obrigado,\nIntelligent Habitacion Admin Team.";

            return plainText;
        }
        private string BodyHtmlText(string adminName, string code)
        {
            var htmlText = $@"<div style=""margin-top: 50px;"">
			<span style=""font-family: 'Raleway';font-size: 14px;"">Olá {adminName},</span>
			<span style=""font-family: 'Raleway';font-size: 14px;display: block;margin-top: 14px;"">Use o código abaixo para definir seu amigo como o novo Administrador:</span>
			
			<div style=""margin-top: 50px;"">
				<span style=""color: #FEBF3B;font-family: 'Raleway';font-size: 30px;font-weight: 800;"">{code}</span>
			</div>
			
			<div style=""margin-top: 50px;"">
				<span style=""font-family: 'Raleway';font-size: 14px;"">Mas lembre-se, não deixe pra depois, pois este código será valido por apenas 10 minutos, combinado?</span>
			</div>
		</div>";

            htmlText = $@"{htmlText}<div style=""margin-top: 100px;"">
			<span style=""font-family: 'Raleway';font-size: 14px;"">Obrigado,</span>
			<span style=""font-family: 'Raleway';font-size: 14px;display: block;margin-top: 14px;"">Intelligent Habitacion Admin Team.</span>
		</div>";

            return htmlText;
        }
    }
}
