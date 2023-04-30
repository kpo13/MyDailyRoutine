using System.Runtime.Serialization;

namespace MyDailyRoutine.Domain.Exceptions
{
    [Serializable]
    public class SimilarGoalAlreadyExistsException : Exception
    {
        public SimilarGoalAlreadyExistsException()
        {
        }

        public SimilarGoalAlreadyExistsException(string name)
            : base($"A goal with the name '{name}' already exists.")
        {
        }

        public SimilarGoalAlreadyExistsException(string name, Exception innerException)
            : base($"A goal with the name '{name}' already exists.", innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected SimilarGoalAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
