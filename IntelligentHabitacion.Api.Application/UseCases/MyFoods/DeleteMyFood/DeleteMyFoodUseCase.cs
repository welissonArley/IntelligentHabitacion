using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using IntelligentHabitacion.Exception;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.MyFoods.DeleteMyFood
{
    public class DeleteMyFoodUseCase : IDeleteMyFoodUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;
        private readonly IMyFoodsReadOnlyRepository _repositoryReadOnly;
        private readonly IMyFoodsWriteOnlyRepository _repository;

        public DeleteMyFoodUseCase(IMyFoodsReadOnlyRepository repositoryReadOnly,
            IMyFoodsWriteOnlyRepository repository, IUnitOfWork unitOfWork, IntelligentHabitacionUseCase intelligentHabitacionUseCase,
            ILoggedUser loggedUser)
        {
            _repositoryReadOnly = repositoryReadOnly;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseOutput> Execute(long myFoodId)
        {
            var loggedUser = await _loggedUser.User();

            var model = await _repositoryReadOnly.GetById(myFoodId, loggedUser.Id);
            if (model is null)
                throw new ProductNotFoundException();

            _repository.Delete(model);

            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
