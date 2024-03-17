using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argus.Events
{
    public class ArgusEventArgs : EventArgs
    {
        public ArgusEvent ArgusEvent { get; set; }

        public ArgusEventArgs(ArgusEvent argusEvent)
        {
            ArgusEvent = argusEvent;
        }
    }
}
