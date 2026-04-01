using Microsoft.EntityFrameworkCore;
using RunEmpire.Data;
using RunEmpire.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunEmpire.Services
{
    public class LoopService
    {
        private readonly AppDbContext _db;

        public LoopService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckLoop(Guid runId)
        {
            var points = await _db.RunPoints
                .Where(x => x.RunId == runId)
                .OrderBy(x => x.Id)
                .ToListAsync();

            if (points.Count < 10)
                return false;

            var last = points.Last();

            for (int i = 0; i < points.Count - 10; i++)
            {
                var distance = Distance(
                    last.Latitude,
                    last.Longitude,
                    points[i].Latitude,
                    points[i].Longitude);

                if (distance < 20)
                {
                    var polygon = points.Skip(i).ToList();

                    await CreateTerritory(polygon);

                    return true;
                }
            }

            return false;
        }

        private async Task CreateTerritory(List<RunPoint> polygon)
        {

            var area = GeoService.CalculateArea(polygon);
            if (area < 200) // ignore small loops
                return;

            var newterritory = new Territory
            {
                Id = Guid.NewGuid(),
                OwnerUserId = Guid.NewGuid(),
                Area = area,
                Power = Math.Round(area * 0.05, 2),
                CreatedAt = DateTime.UtcNow
            };

            _db.Territories.Add(newterritory);

            int index = 0;

            foreach (var p in polygon)
            {
                _db.TerritoryPoints.Add(new TerritoryPoint
                {
                    TerritoryId = newterritory.Id,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    OrderIndex = index++
                });
            }
            var existing = await _db.Territories.ToListAsync();

            foreach (var territory in existing)
            {
                var points = await _db.TerritoryPoints
                    .Where(x => x.TerritoryId == territory.Id)
                    .OrderBy(x => x.OrderIndex)
                    .ToListAsync();

                var first = polygon.First();

                bool inside = PolygonService.IsPointInside(
                    first.Latitude,
                    first.Longitude,
                    points);

                if (inside && territory.Power > territory.Power)
                {
                    // capture enemy territory
                    _db.Territories.Remove(territory);
                }
            }
            await _db.SaveChangesAsync();
        }

        private double Distance(
            double lat1, double lon1,
            double lat2, double lon2)
        {
            var R = 6371000;

            var dLat = ToRad(lat2 - lat1);
            var dLon = ToRad(lon2 - lon1);

            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRad(lat1)) *
                Math.Cos(ToRad(lat2)) *
                Math.Sin(dLon / 2) *
                Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private double ToRad(double v)
            => v * Math.PI / 180;
    }
}
