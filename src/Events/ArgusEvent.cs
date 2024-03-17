using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argus.Events
{
    public class ArgusEvent
    {
        public string Action { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public string ActionDescription { get; set; }
    }
}
