using Microsoft.AspNetCore.Mvc;
using MyDailyRoutine.Application;
using MyDailyRoutine.WebApi.RequestsModel;

namespace MyDailyRoutine.WebApi.Controllers
{
    [Route("habits")]
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public HabitsController(IGoalService goalService)
        {
            this._goalService = goalService;
        }

        [HttpPost]
        public async Task AddHabit([FromBody] AddHabit habit)
        {
            await _goalService.AddHabitAsync(habit.ParentGoalId, habit.Name);
        }

        [HttpDelete("{id}")]
        public async Task RemoveHabit(Guid id)
        {
            await _goalService.RemoveHabitAsync(id);
        }
    }
}
