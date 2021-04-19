namespace IntelligentHabitacion.Exception.ExceptionsBase
{
#pragma warning disable S3925
    public class NotFoundException : IntelligentHabitacionException
    {
        public NotFoundException(string mensage) : base(mensage)
        {
        }
    }
}
