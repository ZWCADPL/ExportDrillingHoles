using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.DatabaseServices;

namespace DrillingHoles
{
    public class ZWHole
    {
        public ZWHole(ObjectId Circle)
        {
            this.Circle = Circle;
            Description = ObjectId.Null;
            ID = ObjectId.Null;
        }


        public ObjectId Circle;
        public ObjectId Description;
        public ObjectId ID;

        Point Position
        {
            get
            {
                return new Point();
            }
        }

        string IDText
        {
            get
            {
                return "";
            }
        }
        string DescriptionText
        {
            get
            {
                return "";
            }
        }

    }
}
