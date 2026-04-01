using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunEmpire.Entities
{
    public class Territory
    {
        public Guid Id { get; set; }

        public Guid OwnerUserId { get; set; }

        public double Area { get; set; }
        public double Power { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
