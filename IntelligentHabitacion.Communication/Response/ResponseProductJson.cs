namespace IntelligentHabitacion.Communication.Response
{
    public enum Type
    {
        Unity = 0,
        Box = 1,
        Package = 2,
        Kilogram = 3
    }

    public class ResponseProductJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Manufacturer { get; set; }
        public Type Type { get; set; }
    }
}
