using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Communication.Error;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Exception.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentHabitacion.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        protected void WriteAutenticationHeader(ResponseOutput response)
        {
            Response.Headers.Add("Tvih", response.Token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        protected void VerifyParameters(object parameter)
        {
            var parameterIsString = parameter is string;
            if (parameterIsString)
            {
                if (string.IsNullOrWhiteSpace((string)parameter))
                    throw new ParametersEmptyOrNullException();
            }
            else if (parameter == null)
                throw new ParametersEmptyOrNullException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        protected ObjectResult HandleException(System.Exception exception)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (!((exception as IntelligentHabitacionException) is null))
                return HandleIntelligentHabitacionException((IntelligentHabitacionException)exception);

            return ThrowUnknowError();
        }

        private ObjectResult HandleIntelligentHabitacionException(IntelligentHabitacionException exception)
        {
            if (!((exception as ErrorOnValidationException) is null))
            {
                ErrorOnValidationException validacaoException = (ErrorOnValidationException)exception;
                return BadRequest(CreateErrorJson(validacaoException));
            }
            else if (!((exception as NotFoundException) is null))
                return NotFound(CreateErrorJson(exception));
            else if (!((exception as InvalidLoginException) is null))
                return Unauthorized(CreateErrorJson(exception));

            return BadRequest(CreateErrorJson(exception));
        }

        private ErrorJson CreateErrorJson(IntelligentHabitacionException exception)
        {
            return new ErrorJson(exception.Message);
        }

        private ErrorJson CreateErrorJson(ErrorOnValidationException exception)
        {
            return new ErrorJson(exception.ErrorMensages);
        }

        private ObjectResult ThrowUnknowError()
        {
            return StatusCode(500, new ErrorJson(ResourceTextException.UNKNOW_ERROR));
        }
    }
}
