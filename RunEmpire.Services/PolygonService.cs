using RunEmpire.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunEmpire.Services
{
    public static class PolygonService
    {
        public static bool IsPointInside(
            double lat,
            double lng,
            List<TerritoryPoint> polygon)
        {
            bool inside = false;

            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                var xi = polygon[i].Latitude;
                var yi = polygon[i].Longitude;

                var xj = polygon[j].Latitude;
                var yj = polygon[j].Longitude;

                var intersect =
                    ((yi > lng) != (yj > lng)) &&
                    (lat < (xj - xi) * (lng - yi) / (yj - yi) + xi);

                if (intersect)
                    inside = !inside;
            }

            return inside;
        }
    }
}
