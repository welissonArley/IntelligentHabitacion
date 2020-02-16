namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseLocationJson
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public ResponseStateJson State { get; set; }
    }

    public class ResponseStateJson
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public ResponseCountryJson Country { get; set; }
    }

    public class ResponseCountryJson
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
