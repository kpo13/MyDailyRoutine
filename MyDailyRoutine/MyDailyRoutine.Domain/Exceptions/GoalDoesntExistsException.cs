using System.Runtime.Serialization;

namespace MyDailyRoutine.Domain.Exceptions
{
    [Serializable]
    public class GoalDoesntExistsException : Exception
    {
        public GoalDoesntExistsException()
        {
        }

        public GoalDoesntExistsException(Guid id)
            : base($"No goal has been found with the id '{id}'.")
        {
        }

        public GoalDoesntExistsException(Guid id, Exception innerException)
            : base($"No goal has been found with the id '{id}'.", innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected GoalDoesntExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
