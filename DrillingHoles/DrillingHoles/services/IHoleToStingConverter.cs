using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillingHoles
{
    public interface IHoleToStingConverter
    {
        string AsString(Hole hole);
    }
}
