using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Enums;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.Services;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangeAdministrator
{
    public class ChangeAdministratorUseCase : IChangeAdministratorUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly ICodeWriteOnlyRepository _codeRepository;
        private readonly PasswordEncripter _cryptography;
        private readonly ICodeReadOnlyRepository _codeReadOnlyRepository;

        public ChangeAdministratorUseCase(ILoggedUser loggedUser, IPushNotificationService pushNotificationService,
            IUnitOfWork unitOfWork, ICodeWriteOnlyRepository codeRepository, PasswordEncripter cryptography,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUserUpdateOnlyRepository repository,
            ICodeReadOnlyRepository codeReadOnlyRepository)
        {
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _pushNotificationService = pushNotificationService;
            _repository = repository;
            _codeRepository = codeRepository;
            _cryptography = cryptography;
            _codeReadOnlyRepository = codeReadOnlyRepository;
        }

        public async Task<ResponseOutput> Execute(long friendId, RequestAdminActionJson request)
        {
            var loggedUser = await _loggedUser.User();

            var friend = await _repository.GetById_Update(friendId);
            var pushNotificationId = friend.PushNotificationId;

            await ValidateActionOnFriend(loggedUser, friend, request);

            friend.HomeAssociation.Home.AdministratorId = friendId;
            _repository.Update(friend);

            _codeRepository.DeleteAllFromTheUser(loggedUser.Id);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            SendNotification(pushNotificationId);

            return response;
        }

        private void SendNotification(string pushNotificationId)
        {
            var titles = new Dictionary<string, string>
            {
                { "en", "Congratulations new Admin 🦁" },
                { "pt", "Parabéns novo Admin 🦁" }
            };
            var messages = new Dictionary<string, string>
            {
                { "en", "You are now the new administrator. Good luck 🏁" },
                { "pt", "Você agora é o novo Administrador. Boa sorte 🏁" }
            };
            var data = new Dictionary<string, string> { { EnumNotifications.NewAdmin, "1" } };

            _pushNotificationService.Send(titles, messages, new List<string> { pushNotificationId }, data);
        }

        private async Task ValidateActionOnFriend(User loggedUser, User friend, RequestAdminActionJson request)
        {
            if (friend == null)
                throw new FriendNotFoundException();

            if (friend.HomeAssociation == null || friend.HomeAssociation.HomeId != loggedUser.HomeAssociation.HomeId)
                throw new YouCannotPerformThisActionException();

            if (!loggedUser.Password.Equals(_cryptography.Encrypt(request.Password)))
                throw new CodeOrPasswordInvalidException();

            var userCode = await _codeReadOnlyRepository.GetByCode(request.Code);

            if (userCode == null || userCode.Type != CodeType.ChangeAdministrator)
                throw new CodeOrPasswordInvalidException();

            if (userCode.CreateDate.AddMinutes(10) < DateTime.UtcNow)
                throw new ExpiredCodeException();
        }
    }
}
