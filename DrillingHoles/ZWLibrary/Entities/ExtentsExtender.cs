using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;

namespace ZWLibrary
{
    public static class ExtentsExtender
    {
        public static Extents3d ExpandBy(this Extents3d ext, double size)
        {
            Vector3d v = new Vector3d(size, size, size);
            Point3d max = ext.MaxPoint + v;
            Point3d min = ext.MinPoint - v;
            Extents3d result = new Extents3d(min, max);
            return result;
        }

        public static bool Contains(this Extents3d ext, Point3d px)
        {
            if (px.X < ext.MinPoint.X) return false;
            if (px.Y < ext.MinPoint.Y) return false;
            if (px.X > ext.MaxPoint.X) return false;
            if (px.Y > ext.MaxPoint.Y) return false;
            return true;
        }

    }
}
