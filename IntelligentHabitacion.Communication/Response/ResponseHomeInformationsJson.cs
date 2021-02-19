using IntelligentHabitacion.Communication.Enums;

namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseHomeInformationsJson
    {
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalAddressInfo { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public CountryEnum Country { get; set; }
        public ResponseWifiNetworkJson NetWork { get; set; }
    }
}
