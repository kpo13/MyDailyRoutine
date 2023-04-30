using Microsoft.AspNetCore.Mvc;
using MyDailyRoutine.Application;
using MyDailyRoutine.Application.Dtos;

namespace MyDailyRoutine.WebApi.Controllers
{
    [Route("goals")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalsController(IGoalService goalService)
        {
            this._goalService = goalService;
        }

        [HttpGet]
        public async Task<IEnumerable<GoalDto>> GetAsync()
        {
            return await this._goalService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<GoalDto> GetAsync(Guid id)
        {
            return await this._goalService.GetAsync(id);
        }

        [HttpPost]
        public async Task PostAsync([FromBody] string name)
        {
            await _goalService.AddAsync(name);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await this._goalService.DeleteAsync(id);
        }
    }
}
