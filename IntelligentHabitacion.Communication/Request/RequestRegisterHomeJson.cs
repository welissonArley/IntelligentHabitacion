namespace IntelligentHabitacion.Communication.Request
{
    public class RequestRegisterHomeJson
    {
        public string ZipCode { get; set; }
        public RequestRegisterCityJson City { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string NetworksName { get; set; }
        public string NetworksPassword { get; set; }
    }

    public class RequestRegisterCityJson
    {
        public string Name { get; set; }
        public RequestRegisterStateJson State { get; set; }
    }
    public class RequestRegisterStateJson
    {
        public string Name { get; set; }
        public RequestRegisterCountryJson Country { get; set; }
    }
    public class RequestRegisterCountryJson
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
