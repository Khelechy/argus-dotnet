using Argus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace argus_dotnet.src.Helpers
{
    internal static class Helpers
    {
        public static (bool, ArgusEvent?, string) IsJsonString(string str)
        {
            try
            {
                var argusEvent = JsonSerializer.Deserialize<ArgusEvent>(str);
                return (true, argusEvent, str);
            }catch (Exception ex)
            {
                return (false, null, str);
            }
        }
    }
}
