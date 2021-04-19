using IntelligentHabitacion.Exception.ExceptionsBase;

#pragma warning disable S3925
namespace IntelligentHabitacion.Exception.API
{
    public class YouCannotPerformThisActionException : IntelligentHabitacionException
    {
        public YouCannotPerformThisActionException() : base(ResourceTextException.YOU_CANNNOT_PERMORM_THIS_ACTION)
        {

        }
    }
}
