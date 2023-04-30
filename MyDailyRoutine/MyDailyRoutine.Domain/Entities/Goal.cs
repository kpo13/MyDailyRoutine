namespace MyDailyRoutine.Domain.Entities
{
    public class Goal
    {
        public Goal(string name)
        {
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
