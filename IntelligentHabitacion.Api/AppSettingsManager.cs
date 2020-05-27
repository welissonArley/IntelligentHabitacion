using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IntelligentHabitacion.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class AppSettingsManager
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public AppSettingsManager(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ConnectionString()
        {
            if (_hostingEnvironment.IsDevelopment())
                return "Server=localhost;Database=intelligenthabitacion;Uid=root;Pwd=@Ioasys;";
            else if (_hostingEnvironment.IsProduction())
                return "";

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string KeyAdditionalCryptography()
        {
            if (_hostingEnvironment.IsDevelopment())
                return "0xYAnUSaoXUjQBJFHrpanwl9DctLyY8";
            else if (_hostingEnvironment.IsProduction())
                return "";

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double ExpirationTimeMinutes()
        {
            if (_hostingEnvironment.IsDevelopment())
                return 180; //3 hours
            else if (_hostingEnvironment.IsProduction())
                return 4320; // 3 days

            return 180;
        }
    }
}
