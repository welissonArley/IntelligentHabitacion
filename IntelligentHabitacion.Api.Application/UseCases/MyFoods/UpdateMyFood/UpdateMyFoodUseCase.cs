using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.SharedValidators;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.MyFoods.UpdateMyFood
{
    public class UpdateMyFoodUseCase : IUpdateMyFoodUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;
        private readonly IMyFoodsUpdateOnlyRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMyFoodUseCase(IMyFoodsUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IntelligentHabitacionUseCase intelligentHabitacionUseCase,
            ILoggedUser loggedUser, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseOutput> Execute(long myFoodId, RequestProductJson editMyFood)
        {
            Validate(editMyFood);

            var loggedUser = await _loggedUser.User();

            var model = await _repository.GetById_Update(myFoodId, loggedUser.Id);
            if (model is null)
                throw new ProductNotFoundException();

            _mapper.Map(editMyFood, model);

            _repository.Update(model);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }

        private void Validate(RequestProductJson requestMyFood)
        {
            var validation = new MyFoodValidation().Validate(requestMyFood);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}
