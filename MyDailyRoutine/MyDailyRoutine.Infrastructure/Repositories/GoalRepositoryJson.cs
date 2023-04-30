using MyDailyRoutine.Application.Dtos;
using MyDailyRoutine.Application.Repositories;
using MyDailyRoutine.Domain.Entities;
using MyDailyRoutine.Domain.Exceptions;
using System.Text.Json;

namespace MyDailyRoutine.Infrastructure.Repositories
{
    public class GoalRepositoryJson : IGoalRepository
    {
        private readonly string _filePath;

        public GoalRepositoryJson(string filePath)
        {
            this._filePath = filePath;
        }

        public async Task<IReadOnlyCollection<GoalDto>> GetAllAsync()
        {
            string json;
            if (File.Exists(_filePath + ".goals.json"))
            {
                using (var fileReader = File.OpenText(_filePath + ".goals.json"))
                {
                    json = await fileReader.ReadToEndAsync();
                }
            } 
            else
            {
                return new List<GoalDto>();
            }

            List<GoalDto> result = new List<GoalDto>();
            if (!string.IsNullOrEmpty(json))
            {
                var existingGoals = JsonSerializer.Deserialize<List<Goal>>(json);
                if (existingGoals is not null)
                {
                    foreach(var goal in existingGoals)
                    {
                        result.Add(new GoalDto(goal.Name)
                        {
                            Id = goal.Id,
                        });
                    }
                }
            }

            return result;
        }

        public async Task AddAsync(GoalDto goal)
        {
            string json;
            List<Goal> goals = new List<Goal>();

            if (File.Exists(_filePath + ".goals.json"))
            {
                using (var fileReader = File.OpenText(_filePath + ".goals.json"))
                {
                    json = await fileReader.ReadToEndAsync();
                }

                if (!string.IsNullOrEmpty(json))
                {
                    var existingGoals = JsonSerializer.Deserialize<List<Goal>>(json);
                    if (existingGoals is not null && existingGoals.Count > 0)
                    {
                        goals = existingGoals.ToList();
                    }
                }
            }

            var newId = Guid.NewGuid();
            goals.Add(
                new Goal(goal.Name)
                {
                    Id = newId,
                }
            );
            goal.Id = newId;

            using var fileWriter = File.CreateText(_filePath + ".goals.json");
            await fileWriter.WriteAsync(JsonSerializer.Serialize(goals));
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            string json;
            if (File.Exists(_filePath + ".goals.json"))
            {
                using (var fileReader = File.OpenText(_filePath + ".goals.json"))
                {
                    json = await fileReader.ReadToEndAsync();
                }
            }
            else
            {
                throw new GoalDoesntExistsException(id);
            }

            List<GoalDto> result = new List<GoalDto>();
            if (!string.IsNullOrEmpty(json))
            {
                var existingGoals = JsonSerializer.Deserialize<List<Goal>>(json);
                if (existingGoals is not null)
                {
                    foreach (var goal in existingGoals)
                    {
                        result.Add(new GoalDto(goal.Name)
                        {
                            Id = goal.Id,
                        });
                    }
                }
            }

            if (result.Count == 0)
            {
                throw new GoalDoesntExistsException(id);
            }

            result.RemoveAll(g => g.Id == id);

            using var fileWriter = File.CreateText(_filePath + ".goals.json");
            await fileWriter.WriteAsync(JsonSerializer.Serialize(result));
        }

        public async Task<GoalDto> GetByIdAsync(Guid id)
        {
            string json;
            if (File.Exists(_filePath + ".goals.json"))
            {
                using (var fileReader = File.OpenText(_filePath + ".goals.json"))
                {
                    json = await fileReader.ReadToEndAsync();
                }
            }
            else
            {
                throw new GoalDoesntExistsException(id);
            }

            Goal? result = null;

            if (!string.IsNullOrEmpty(json))
            {
                var existingGoals = JsonSerializer.Deserialize<List<Goal>>(json);
                if (existingGoals is not null && existingGoals.Count > 0)
                {
                    result = existingGoals.FirstOrDefault(g => g.Id == id);
                }
            }

            if (result != null)
            {
                return new GoalDto(result.Name)
                {
                    Id = result.Id
                };
            }

            throw new GoalDoesntExistsException(id);
        }
    }
}
