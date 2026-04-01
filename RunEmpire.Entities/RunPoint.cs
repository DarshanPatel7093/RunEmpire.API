using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunEmpire.Entities
{
    public class RunPoint
    {
        public long Id { get; set; }

        public Guid RunId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Speed { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
