using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace DrillingHoles
{
    class ZWHoleToHole
    {
        private ZWHole h;
        private Transaction tr;

        public ZWHoleToHole(ZWHole h, Transaction tr)
        {
            this.h = h;
            this.tr = tr;
        }

        public Hole Hole
        {
            get
            {
                return new Hole(Position(h), ID(h), Description(h) );
            }
        }


        private Point Position(ZWHole h)
        {
            if (h.Circle.IsNull)
                throw new ArgumentNullException();
            Circle c = tr.GetObject(h.Circle, OpenMode.ForRead) as Circle;
            if (c is null)
                throw new InvalidCastException();
            return new Point(c.Center.X, c.Center.Y);
        }
        private string ID(ZWHole h)
        {
            if (h.ID.IsNull)
                return "";
            DBText txt = tr.GetObject(h.ID, OpenMode.ForRead) as DBText;
            if (txt is null)
                return "";
            return txt.TextString ;
        }

        private string Description(ZWHole h)
        {
            if (h.Description.IsNull)
                return "";
            DBText txt = tr.GetObject(h.Description, OpenMode.ForRead) as DBText;
            if (txt is null)
                return "";
            return txt.TextString;
        }

        internal static Hole Convert(ZWHole h, Transaction tr)
        {
            ZWHoleToHole converter = new  ZWHoleToHole(h, tr);
            return converter.Hole;
        }
    }
}
