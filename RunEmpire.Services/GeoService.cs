using RunEmpire.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunEmpire.Services
{
    public static class GeoService
    {
        private const double EarthRadius = 6378137; // meters

        public static double CalculateArea(List<RunPoint> points)
        {
            if (points == null || points.Count < 3)
                return 0;

            var projected = points
                .Select(p => Project(p.Latitude, p.Longitude))
                .ToList();

            double area = 0;

            for (int i = 0; i < projected.Count; i++)
            {
                var (x1, y1) = projected[i];
                var (x2, y2) = projected[(i + 1) % projected.Count];

                area += (x1 * y2) - (x2 * y1);
            }

            return Math.Abs(area) / 2.0;
        }

        private static (double x, double y) Project(double lat, double lng)
        {
            var x = EarthRadius * ToRad(lng);
            var y = EarthRadius * Math.Log(
                Math.Tan(Math.PI / 4 + ToRad(lat) / 2));

            return (x, y);
        }

        private static double ToRad(double deg)
            => deg * Math.PI / 180;
    }
}
