namespace MyDailyRoutine.WebApi.RequestsModel
{
    public class AddHabit
    {
        public AddHabit(Guid parentGoalId, string name)
        {
            this.ParentGoalId = parentGoalId;
            this.Name = name;
        }

        public Guid ParentGoalId { get; }
        public string Name { get; }
    }
}
