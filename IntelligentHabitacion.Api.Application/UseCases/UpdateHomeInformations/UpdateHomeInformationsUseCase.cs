using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Home;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.UpdateHomeInformations
{
    public class UpdateHomeInformationsUseCase : IUpdateHomeInformationsUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHomeUpdateOnlyRepository _repository;

        public UpdateHomeInformationsUseCase(ILoggedUser loggedUser, IMapper mapper, IUnitOfWork unitOfWork,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IHomeUpdateOnlyRepository repository)
        {
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseOutput> Execute(RequestUpdateHomeJson updateHomeJson)
        {
            var validation = new UpdateHomeInformationValidation().Validate(updateHomeJson);

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());

            var loggedUser = await _loggedUser.User();
            
            var homeModel = await _repository.GetById_Update(loggedUser.HomeAssociation.HomeId);
            _mapper.Map(updateHomeJson, homeModel);

            _repository.Update(homeModel);
            
            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
