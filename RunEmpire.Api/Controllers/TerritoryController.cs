using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunEmpire.Data;

namespace RunEmpire.Controllers;

[ApiController]
[Route("api/territories")]
public class TerritoryController : ControllerBase
{
    private readonly AppDbContext _db;

    public TerritoryController(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Get all territories with polygon points
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var territories = await _db.Territories
            .Select(t => new
            {
                id = t.Id,
                ownerUserId = t.OwnerUserId,
                area = t.Area,
                createdAt = t.CreatedAt,

                points = _db.TerritoryPoints
                    .Where(p => p.TerritoryId == t.Id)
                    .OrderBy(p => p.OrderIndex)
                    .Select(p => new
                    {
                        latitude = p.Latitude,
                        longitude = p.Longitude
                    })
                    .ToList()
            })
            .ToListAsync();

        return Ok(territories);
    }
}