using AutoMapper;
using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.RegisterMyFood
{
    public class RegisterMyFoodUseCase : IRegisterMyFoodUseCase
    {
        private readonly IHashids _hashIds;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IMyFoodsWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterMyFoodUseCase(IMyFoodsWriteOnlyRepository repository, IUnitOfWork unitOfWork,
            ILoggedUser loggedUser, IMapper mapper, IntelligentHabitacionUseCase intelligentHabitacionUseCase,
            IHashids hashIds)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _hashIds = hashIds;
        }

        public async Task<ResponseOutput> Execute(RequestAddMyFoodJson requestMyFood)
        {
            Validate(requestMyFood);

            var loggedUser = await _loggedUser.User();

            var foodModel = _mapper.Map<MyFood>(requestMyFood);
            foodModel.UserId = loggedUser.Id;

            await _repository.Add(foodModel);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            response.ResponseJson = _hashIds.EncodeLong(foodModel.Id);

            return response;
        }

        private void Validate(RequestAddMyFoodJson requestMyFood)
        {
            var validation = new RegisterFoodValidation().Validate(requestMyFood);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
