﻿using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Api.Domain.ValueObjects;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.Friends.RemoveFriend
{
    public class RequestCodeToRemoveFriendUseCase : IRequestCodeToRemoveFriendUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICodeWriteOnlyRepository _repository;
        private readonly ISendEmail _emailHelper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestCodeToRemoveFriendUseCase(ILoggedUser loggedUser, ICodeWriteOnlyRepository repository,
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
                Type = Domain.Enums.CodeType.RemoveFriend,
                Value = codeRandom,
                UserId = loggedUser.Id
            });

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            await _emailHelper.Send(new EmailContent
            {
                SendToEmail = loggedUser.Email,
                Subject = "Remover amigo",
                HtmlText = BodyHtmlText(loggedUser.Name, codeRandom),
                PlainText = BodyPlainText(loggedUser.Name, codeRandom)
            });

            return response;
        }

        private string BodyPlainText(string adminName, string code)
        {
            var plainText = $"Olá {adminName}, Use o código abaixo para remover seu amigo da associação com a Home:\n\n\n";
            plainText = $"{plainText}{code}\n\n\n";
            plainText = $"{plainText}Mas lembre-se, não deixe pra depois pois este código será valido por apenas 10 minutos, combinado?\n\n\n";
            plainText = $"{plainText}Obrigado,\nIntelligent Habitacion Admin Team.";

            return plainText;
        }
        private string BodyHtmlText(string adminName, string code)
        {
            var htmlText = $@"<div style=""margin-top: 50px;"">
			<span style=""font-family: 'Raleway';font-size: 14px;"">Olá {adminName},</span>
			<span style=""font-family: 'Raleway';font-size: 14px;display: block;margin-top: 14px;"">Use o código abaixo para remover seu amigo da associação com a Home:</span>
			
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
