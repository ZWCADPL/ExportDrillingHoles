using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillingHoles
{
    public abstract class HolesRepository
    {
        protected List<Hole> _holes = new List<Hole>();

        public List<Hole> Holes {
            get
            {
                return _holes;
            }
        }

        public void Add(Hole hole)
        {
            _holes.Add(hole);
        }
        public void Add(List<Hole> holes)
        {
            _holes.AddRange(holes);
        }

        public abstract void Save();
    }
}
