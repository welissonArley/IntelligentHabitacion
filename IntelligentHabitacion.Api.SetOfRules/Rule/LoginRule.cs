﻿using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Cryptography;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Useful;
using IntelligentHabitacion.Validators.Validator;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class LoginRule : ILoginRule
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyPassword _cryptography;
        private readonly ICodeRepository _codeRepository;

        public LoginRule(IUserRepository userRepository, ICryptographyPassword cryptography, ICodeRepository codeRepository)
        {
            _userRepository = userRepository;
            _cryptography = cryptography;
            _codeRepository = codeRepository;
        }

        public ResponseLoginJson DoLogin(RequestLoginJson loginJson)
        {
            var user = _userRepository.GetByEmail(loginJson.User);

            if (user == null || !user.Password.Equals(_cryptography.Encrypt(loginJson.Password)))
                throw new InvalidLoginException();

            return new Mapper.Mapper().MapperModelToJsonLogin(user);
        }

        public void RequestCodeToResetPassword(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user != null)
            {
                var codeRandom = new CodeGenerator().Random();

                var userCodes = _codeRepository.GetByUser(user.Id);
                foreach(var code in userCodes)
                    _codeRepository.DeleteOnDatabase(code);

                _codeRepository.Create(new Code
                {
                    Active = true,
                    Type = CodeType.ResetPassword,
                    CreateDate = DateTimeController.DateTimeNow(),
                    Value = codeRandom,
                    UserId = user.Id
                });

                new EmailHelper.EmailHelper().ResetPassword(email, codeRandom, user.Name);
            }
        }

        public void ResetYourPassword(RequestResetYourPasswordJson resetYourPasswordJson)
        {
            var user = _userRepository.GetByEmail(resetYourPasswordJson.Email);
            if (user == null)
                throw new InvalidUserException();

            var userCode = _codeRepository.GetByUserResetPassword(user.Id);
            if (userCode == null)
                throw new RequiredCodeResetPasswordException();

            _codeRepository.DeleteOnDatabase(userCode);

            if(!userCode.Value.Equals(resetYourPasswordJson.Code.ToUpper()) || userCode.CreateDate.AddHours(1) < DateTimeController.DateTimeNow())
                throw new RequiredCodeResetPasswordException();

            new PasswordValidator().IsValidaPasswordAndConfirmation(resetYourPasswordJson.Password, resetYourPasswordJson.PasswordConfirmation);

            user.Password = _cryptography.Encrypt(resetYourPasswordJson.Password);

            _userRepository.Update(user);
        }
    }
}
