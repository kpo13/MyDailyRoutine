using MyDailyRoutine.Application.Dtos;
using MyDailyRoutine.Domain.Entities;

namespace MyDailyRoutine.Application.Repositories
{
    public interface IHabitRepository
    {
        Task AddAsync(HabitDto habit);
        Task DeleteByIdAsync(Guid id);
        Task<IEnumerable<HabitDto>> GetAllAsync(Guid id);
        Task<HabitDto> GetByIdAsync(Guid id);
    }
}
