using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunEmpire.Entities
{
    public class Run
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool IsCompleted { get; set; }
    }
}
