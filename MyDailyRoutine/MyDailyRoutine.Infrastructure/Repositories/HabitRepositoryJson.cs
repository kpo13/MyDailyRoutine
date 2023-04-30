using MyDailyRoutine.Application.Dtos;
using MyDailyRoutine.Application.Repositories;
using MyDailyRoutine.Domain.Entities;
using MyDailyRoutine.Domain.Exceptions;
using System.Text.Json;

namespace MyDailyRoutine.Infrastructure.Repositories
{
    public class HabitRepositoryJson : IHabitRepository
    {
        private readonly string _filePath;

        public HabitRepositoryJson(string filePath)
        {
            this._filePath = filePath;
        }

        public async Task AddAsync(HabitDto habit)
        {
            string json;
            List<Habit> habits = new List<Habit>();

            if (File.Exists(_filePath + ".habits.json"))
            {
                using (var fileReader = File.OpenText(_filePath + ".habits.json"))
                {
                    json = await fileReader.ReadToEndAsync();
                }

                if (!string.IsNullOrEmpty(json))
                {
                    var existingHabits = JsonSerializer.Deserialize<List<Habit>>(json);
                    if (existingHabits is not null && existingHabits.Count > 0)
                    {
                        habits = existingHabits.ToList();
                    }
                }
            }

            var newId = Guid.NewGuid();
            habits.Add(
                new Habit(habit.GoalId, habit.Name)
                {
                    Id = newId,
                }
            );
            habit.Id = newId;

            using var fileWriter = File.CreateText(_filePath + ".habits.json");
            await fileWriter.WriteAsync(JsonSerializer.Serialize(habits));
        }

        public async Task<IEnumerable<HabitDto>> GetAllAsync(Guid id)
        {
            string json;
            if (File.Exists(_filePath + ".habits.json"))
            {
                using (var fileReader = File.OpenText(_filePath + ".habits.json"))
                {
                    json = await fileReader.ReadToEndAsync();
                }
            }
            else
            {
                return new List<HabitDto>();
            }

            List<HabitDto> result = new List<HabitDto>();
            if (!string.IsNullOrEmpty(json))
            {
                var existingHabits = JsonSerializer.Deserialize<List<Habit>>(json);
                if (existingHabits is not null)
                {
                    var matchingGoal = existingHabits.Where(h => h.GoalId == id).ToList();
                    foreach (var habit in matchingGoal)
                    {
                        result.Add(new HabitDto(habit.GoalId, habit.Name)
                        {
                            Id = habit.Id,
                        });
                    }
                }
            }

            return result;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            string json;
            List<Habit> habits = new List<Habit>();

            if (File.Exists(_filePath + ".habits.json"))
            {
                using (var fileReader = File.OpenText(_filePath + ".habits.json"))
                {
                    json = await fileReader.ReadToEndAsync();
                }

                if (!string.IsNullOrEmpty(json))
                {
                    var existingHabits = JsonSerializer.Deserialize<List<Habit>>(json);
                    if (existingHabits is not null && existingHabits.Count > 0)
                    {
                        habits = existingHabits.ToList();
                    }
                }
            }

            if (habits.FirstOrDefault(h => h.Id == id) is null)
            {
                throw new HabitDoesntExistsException(id);
            }

            habits.RemoveAll(w => w.Id == id);

            using var fileWriter = File.CreateText(_filePath + ".habits.json");
            await fileWriter.WriteAsync(JsonSerializer.Serialize(habits));
        }

        public async Task<HabitDto> GetByIdAsync(Guid id)
        {
            string json;
            if (File.Exists(_filePath + ".habits.json"))
            {
                using (var fileReader = File.OpenText(_filePath + ".habits.json"))
                {
                    json = await fileReader.ReadToEndAsync();
                }
            }
            else
            {
                throw new HabitDoesntExistsException(id);
            }

            Habit? result = null;

            if (!string.IsNullOrEmpty(json))
            {
                var existingHabits = JsonSerializer.Deserialize<List<Habit>>(json);
                if (existingHabits is not null)
                {
                    result = existingHabits.FirstOrDefault(g => g.Id == id);
                }
            }

            if (result != null)
            {
                return new HabitDto(result.GoalId, result.Name)
                {
                    Id = result.Id
                };
            }

            throw new GoalDoesntExistsException(id);
        }
    }
}
