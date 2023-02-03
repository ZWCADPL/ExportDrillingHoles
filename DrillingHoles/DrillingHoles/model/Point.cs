using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillingHoles
{
    public struct Point
    {
        public double X;
        public double Y;

        public Point(double x = 0.0 , double y = 0.0 ) : this()
        {
            X = x;
            Y = y;
        }
    }
}
