using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunEmpire.Services
{
    public class GridService
    {
        private const double EarthRadius = 6378137;
        private const double GridSize = 50; // 50 meters

        public (long gridX, long gridY) GetGrid(double latitude, double longitude)
        {
            ValidateCoordinates(latitude, longitude);

            double latRad = DegreesToRadians(latitude);
            double lonRad = DegreesToRadians(longitude);

            double x = EarthRadius * lonRad;
            double y = EarthRadius * Math.Log(Math.Tan(Math.PI / 4 + latRad / 2));

            long gridX = (long)Math.Floor(x / GridSize);
            long gridY = (long)Math.Floor(y / GridSize);

            return (gridX, gridY);
        }

        private static double DegreesToRadians(double degrees)
            => degrees * Math.PI / 180;

        private static void ValidateCoordinates(double latitude, double longitude)
        {
            if (latitude < -85 || latitude > 85)
                throw new ArgumentOutOfRangeException(nameof(latitude));

            if (longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException(nameof(longitude));
        }


    }
}
