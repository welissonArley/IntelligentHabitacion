using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Dto;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.CleaningSchedule;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.Calendar
{
    public class CalendarUseCase : ICalendarUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICleaningScheduleReadOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CalendarUseCase(ICleaningScheduleReadOnlyRepository repository, ILoggedUser loggedUser,
            IntelligentHabitacionUseCase intelligentHabitacionUseCase, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOutput> Execute(RequestCalendarCleaningScheduleJson request)
        {
            var loggedUser = await _loggedUser.User();

            var calendar = await _repository.GetCalendarTasksForMonth(request.Month, loggedUser.HomeAssociation.HomeId, request.Room, loggedUser.Id);

            var responseJson = Mapper(request.Month, calendar);
            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id, responseJson);

            await _unitOfWork.Commit();

            return response;
        }

        private ResponseCalendarCleaningScheduleJson Mapper(DateTime date, IList<CleaningScheduleCalendarDayInfoDto> dto)
        {
            return new ResponseCalendarCleaningScheduleJson
            {
                Date = date,
                CleanedDays = dto.Select(c => new ResponseCleaningScheduleCalendarDayInfoJson
                {
                    Day = c.Day,
                    AmountCleanedRecords = c.AmountCleanedRecords,
                    AmountcleanedRecordsToRate = c.AmountcleanedRecordsToRate
                }).ToList()
            };
        }
    }
}
