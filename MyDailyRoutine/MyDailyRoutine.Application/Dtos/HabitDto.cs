namespace MyDailyRoutine.Application.Dtos
{
    public class HabitDto
    {
        public HabitDto(Guid goalId, string name)
        {
            this.GoalId = goalId;
            this.Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get;  }
        public Guid GoalId { get; }
    }
}
