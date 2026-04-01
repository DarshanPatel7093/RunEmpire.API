using Microsoft.AspNetCore.Mvc;
using RunEmpire.Data;
using RunEmpire.Entities;
using RunEmpire.Services;

namespace RunEmpire.Api.Controllers
{
    [ApiController]
    [Route("api/runs")]
    public class RunsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly LoopService _loop;

        public RunsController(AppDbContext db, LoopService loop)
        {
            _db = db;
            _loop = loop;
        }

        [HttpPost("start")]
        public async Task<IActionResult> Start()
        {
            var run = new Run
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.UtcNow,
                IsCompleted = false
            };

            _db.Runs.Add(run);
            await _db.SaveChangesAsync();

            return Ok(new { RunId = run.Id });
        }

        [HttpPost("point")]
        public async Task<IActionResult> Point(RunPoint dto)
        {
            var point = new RunPoint
            {
                RunId = dto.RunId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Speed = dto.Speed,
                CreatedAt = DateTime.UtcNow
            };

            _db.RunPoints.Add(point);
            await _db.SaveChangesAsync();

            var captured = await _loop.CheckLoop(dto.RunId);

            return Ok(new { captured });
        }

        [HttpPost("stop")]
        public async Task<IActionResult> Stop(Guid runId)
        {
            var run = await _db.Runs.FindAsync(runId);

            run.EndTime = DateTime.UtcNow;
            run.IsCompleted = true;

            await _db.SaveChangesAsync();

            return Ok(true);
        }

    }
}
