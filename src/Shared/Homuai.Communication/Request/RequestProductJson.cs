using Homuai.Communication.Enums;

namespace Homuai.Communication.Request
{
    public class RequestProductJson
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Manufacturer { get; set; }
        public Type Type { get; set; }
        public System.DateTime? DueDate { get; set; }
    }
}
