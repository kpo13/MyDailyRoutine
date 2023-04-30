using System.Runtime.Serialization;

namespace MyDailyRoutine.Domain.Exceptions
{
    [Serializable]
    public class SimilarHabitAlreadyExistsException : Exception
    {
        public SimilarHabitAlreadyExistsException()
        {
        }

        public SimilarHabitAlreadyExistsException(string name)
            : base($"A habit with the name '{name}' already exists.")
        {
        }

        public SimilarHabitAlreadyExistsException(string name, Exception innerException)
            : base($"A habit with the name '{name}' already exists.", innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected SimilarHabitAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
