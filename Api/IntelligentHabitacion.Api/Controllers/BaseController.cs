using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Exception.Parameters;
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
    }
}
