using RunEmpire.Data;

namespace RunEmpire.Entities
{
    public class TerritoryPoint
    {
        public long Id { get; set; }

        public Guid TerritoryId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int OrderIndex { get; set; }
    }
}