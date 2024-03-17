using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argus
{
    internal class ArgusException : Exception
    {
        public ArgusException() { }
        public ArgusException(string message) : base(message) { }
        public ArgusException(string message, Exception inner) : base(message, inner) { }
    }
}
