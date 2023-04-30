using MyDailyRoutine.Application.Dtos;
using MyDailyRoutine.Application.Repositories;
using MyDailyRoutine.Domain.Entities;
using MyDailyRoutine.Domain.Exceptions;

namespace MyDailyRoutine.Application
{
    public class GoalService : IGoalService
    {
        private readonly IHabitRepository _habitRepository;
        private readonly IGoalRepository _goalRepository;

        public GoalService(IGoalRepository goalRepository, IHabitRepository habitRepository)
        {
            this._goalRepository = goalRepository;
            this._habitRepository = habitRepository;
        }

        public async Task AddAsync(string name)
        {
            var insertedGoal = new GoalDto(name);

            try
            {
                await this._goalRepository.AddAsync(insertedGoal);
            } 
            catch (SimilarGoalAlreadyExistsException)
            {
                throw new InvalidOperationException("A similar goal already exists");
            }
        }

        public async Task<GoalDto> GetAsync(Guid id)
        {
            try
            {
                return await this._goalRepository.GetByIdAsync(id);
            } 
            catch (GoalDoesntExistsException)
            {
                throw new InvalidOperationException($"No goal has been found for id '{id}'");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            GoalDto goal;

            try
            {
                goal = await this._goalRepository.GetByIdAsync(id);
            } 
            catch (GoalDoesntExistsException)
            {
                throw new InvalidOperationException($"No goal has been found for id '{id}'");
            }

            if (goal.Habits.Any())
            {
                foreach(var habitId in goal.Habits.Select(habit => habit.Id))
                {
                    try
                    {
                        await this._habitRepository.DeleteByIdAsync(habitId);
                    } 
                    catch (HabitDoesntExistsException)
                    {
                        throw new InvalidOperationException($"No habit has been found for id '{habitId}'");
                    }
                }
            }

            await this._goalRepository.DeleteByIdAsync(id);
        }

        public async Task AddHabitAsync(Guid id, string habitName)
        {
            try
            {
                _ = await this._goalRepository.GetByIdAsync(id);
            }
            catch (GoalDoesntExistsException)
            {
                throw new InvalidOperationException($"No goal has been found for id '{id}'");
            }

            var habit = new HabitDto(id, habitName);

            try
            {
                await _habitRepository.AddAsync(habit);
            }
            catch (SimilarHabitAlreadyExistsException)
            {
                throw new InvalidOperationException("A similar goal already exists");
            }
        }

        public async Task RemoveHabitAsync(Guid id)
        {
            await this._habitRepository.DeleteByIdAsync(id);
        }

        public async Task<IReadOnlyCollection<GoalDto>> GetAllAsync()
        {
            var goals = await this._goalRepository.GetAllAsync();
            
            foreach(var goal in goals)
            {
                var habits = await this._habitRepository.GetAllAsync(goal.Id);
                foreach (var hab in habits)
                {
                    goal.Habits.Add(hab);
                }
            }

            return goals;
        }
    }
}
