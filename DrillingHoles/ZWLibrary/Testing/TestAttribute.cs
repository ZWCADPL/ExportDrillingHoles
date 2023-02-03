using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWTests
{   // https://www.plukasiewicz.net/Csharp_dla_zaawansowanych/Atrybuty
    [AttributeUsage(
    AttributeTargets.Method ,
    AllowMultiple = true)]
    public class TestMethod : Attribute
    {
    }
}
