using MyDailyRoutine.Domain.Entities;
using System.Collections;

namespace MyDailyRoutine.Application.Dtos
{
    public class GoalDto
    {
        public GoalDto(string name)
        {
            this.Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<HabitDto> Habits { get; } = new List<HabitDto>();
    }
}
