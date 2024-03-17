using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argus
{
    public class ArgusConfig
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; } = 0;
    }
}
