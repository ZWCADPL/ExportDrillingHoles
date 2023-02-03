using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillingHoles
{
    public struct Hole
    {
        public Point Position { get; private set; }
        public string ID { get; private set; }
        public string description { get; private set; }

        public Hole(Point Position, string ID, string description)
        {
            this.Position = Position;
            this.ID = ID;
            this.description = description;
        }
    }
}
