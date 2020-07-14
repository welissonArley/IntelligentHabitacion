namespace IntelligentHabitacion.Communication.Response
{
    public class ResponseHomeInformationsJson
    {
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public short DeadlinePaymentRent { get; set; }
        public ResponseStateJson State { get; set; }
        public ResponseWifiNetworkJson NetWork { get; set; }
    }
}
