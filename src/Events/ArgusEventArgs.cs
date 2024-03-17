using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argus.Events
{
    public class ArgusEventArgs : EventArgs
    {
        ArgusEvent Event { get; set; }

        public ArgusEventArgs(ArgusEvent argusEvent)
        {
            Event = argusEvent;
        }
    }
}
