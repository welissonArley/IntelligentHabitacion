using IntelligentHabitacion.Useful;

namespace IntelligentHabitacion.Communication.Request
{
    public class RequestHomeJson
    {
        public string ZipCode { get; set; }
        public RequestRegisterCityJson City { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalAddressInfo { get; set; }
        public string Neighborhood { get; set; }
        public short DeadlinePaymentRent { get; set; }
        public string NetworksName { get; set; }
        public string NetworksPassword { get; set; }
    }

    public class RequestRegisterCityJson
    {
        public string Name { get; set; }
        public string StateProvinceName { get; set; }
        public CountryEnum Country { get; set; }
    }
}
