using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.Token;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Enums;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Api.Domain.ValueObjects;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.AddFriends
{
    public class AddFriendUseCase : IAddFriendUseCase
    {
        private readonly IHashids _hashIds;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICodeWriteOnlyRepository _codeWriteRepository;
        private readonly ICodeReadOnlyRepository _codeReadOnlyRepository;
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly IUserUpdateOnlyRepository _userUpdateRepository;
        private readonly TokenController _tokenController;

        public AddFriendUseCase(IHashids hashIds, IUnitOfWork unitOfWork, ICodeWriteOnlyRepository codeWriteRepository,
            ICodeReadOnlyRepository codeReadOnlyRepository, IUserReadOnlyRepository userRepository,
            IUserUpdateOnlyRepository userUpdateRepository, TokenController tokenController)
        {
            _hashIds = hashIds;
            _unitOfWork = unitOfWork;
            _codeWriteRepository = codeWriteRepository;
            _codeReadOnlyRepository = codeReadOnlyRepository;
            _userRepository = userRepository;
            _userUpdateRepository = userUpdateRepository;
            _tokenController = tokenController;
        }

        public async Task ApproveFriend(string adminId, string friendId, RequestApproveAddFriendJson requestApprove)
        {
            if (requestApprove.MonthlyRent <= 0)
                throw new MonthlyRentInvalidException();

            if (DateTime.Compare(requestApprove.JoinedOn.Date, DateTime.UtcNow.Date) > 0)
                throw new InvalidDateException(DateTime.UtcNow);

            var admin = await _userRepository.GetById(_hashIds.DecodeLong(adminId).First());

            var friend = await _userUpdateRepository.GetById_Update(_hashIds.DecodeLong(friendId).First());

            friend.HomeAssociation = new HomeAssociation
            {
                HomeId = admin.HomeAssociation.HomeId,
                MonthlyRent = requestApprove.MonthlyRent,
                JoinedOn = requestApprove.JoinedOn.Date
            };

            _userUpdateRepository.Update(friend);

            await _unitOfWork.Commit();
        }

        public async Task<ResponseCodeWasReadJson> CodeWasRead(string userToken, string code)
        {
            var email = _tokenController.User(userToken);
            var user = await _userRepository.GetByEmail(email);

            if (user.HomeAssociationId != null)
                throw new IntelligentHabitacionException(ResourceTextException.USER_IS_PART_OF_A_HOME);

            var codeResult = await _codeReadOnlyRepository.GetByCode(code);
            if (codeResult == null)
                throw new IntelligentHabitacionException(ResourceTextException.CODE_INVALID);

            var admin = await _userRepository.GetById(codeResult.UserId);

            _codeWriteRepository.DeleteAllFromTheUser(user.Id);

            await _unitOfWork.Commit();

            return new ResponseCodeWasReadJson
            {
                Id = _hashIds.EncodeLong(user.Id),
                Name = user.Name,
                Phonenumbers = user.Phonenumbers.Select(c => new ResponsePhonenumberJson { Number = c.Number }).ToList(),
                EmergencyContacts = user.EmergencyContacts.Select(c => new ResponseEmergencyContactJson
                { Name = c.Name, Relationship = c.Relationship, Phonenumber = c.Phonenumber }).ToList(),
                ProfileColor = user.ProfileColor,
                AdminId = _hashIds.EncodeLong(admin.Id)
            };
        }

        public async Task<ResponseCodeToAddFriendJson> GetCodeToAddFriend(string userToken)
        {
            var email = _tokenController.User(userToken);
            var user = await _userRepository.GetByEmail(email);

            if (user == null || user.HomeAssociationId == null || user.HomeAssociation.Home.AdministratorId != user.Id)
                throw new IntelligentHabitacionException(ResourceTextException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);

            var codeRandom = new CodeGenerator().Random36Chars();

            await _codeWriteRepository.Add(new Code
            {
                Active = true,
                Type = CodeType.AddFriend,
                Value = codeRandom,
                UserId = user.Id
            });

            await _unitOfWork.Commit();

            return new ResponseCodeToAddFriendJson
            {
                AdminId = _hashIds.EncodeLong(user.Id),
                Code = codeRandom
            };
        }
    }
}
