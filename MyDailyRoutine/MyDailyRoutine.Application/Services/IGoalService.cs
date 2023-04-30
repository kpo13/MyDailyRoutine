using MyDailyRoutine.Application.Dtos;

namespace MyDailyRoutine.Application
{
    public interface IGoalService
    {
        // Manage goals
        Task AddAsync(string name);
        Task<GoalDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<IReadOnlyCollection<GoalDto>> GetAllAsync();

        // Manage habits linked to a goal
        Task AddHabitAsync(Guid id, string habitName);
        Task RemoveHabitAsync(Guid id);
    }
}
