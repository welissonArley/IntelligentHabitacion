﻿using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.Cryptography;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.User.RegisterUser
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _repositoryUserReadOnly;
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly PasswordEncripter _cryptography;

        public RegisterUserUseCase(IMapper mapper, IUnitOfWork unitOfWork,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUserWriteOnlyRepository repository,
            IUserReadOnlyRepository repositoryUserReadOnly, PasswordEncripter cryptography)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _repository = repository;
            _repositoryUserReadOnly = repositoryUserReadOnly;
            _cryptography = cryptography;
        }

        public async Task<ResponseOutput> Execute(RequestRegisterUserJson registerUserJson)
        {
            await ValidateRequest(registerUserJson);

            var userModel = _mapper.Map<Domain.Entity.User>(registerUserJson);
            userModel.Password = _cryptography.Encrypt(userModel.Password);

            await _repository.Add(userModel);
            await _unitOfWork.Commit();

            var json = _mapper.Map<ResponseUserRegisteredJson>(userModel);

            var response = await _intelligentHabitacionUseCase.CreateResponse(userModel.Email, userModel.Id, json);

            await _unitOfWork.Commit();

            return response;
        }

        private async Task ValidateRequest(RequestRegisterUserJson registerUserJson)
        {
            var validation = await new RegisterUserValidation(_repositoryUserReadOnly).ValidateAsync(registerUserJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
