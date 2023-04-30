using System.Runtime.Serialization;

namespace MyDailyRoutine.Domain.Exceptions
{
    [Serializable]
    public class HabitDoesntExistsException : Exception
    {
        public HabitDoesntExistsException()
        {
        }

        public HabitDoesntExistsException(Guid id)
            : base($"No habit has been found with the id '{id}'.")
        {
        }

        public HabitDoesntExistsException(Guid id, Exception innerException)
            : base($"No habit has been found with the id '{id}'.", innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected HabitDoesntExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
