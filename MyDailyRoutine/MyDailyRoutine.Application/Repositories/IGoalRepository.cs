using MyDailyRoutine.Application.Dtos;

namespace MyDailyRoutine.Application.Repositories
{
    public interface IGoalRepository
    {
        Task AddAsync(GoalDto goal);
        Task DeleteByIdAsync(Guid id);
        Task<IReadOnlyCollection<GoalDto>> GetAllAsync();
        Task<GoalDto> GetByIdAsync(Guid id);
    }
}
