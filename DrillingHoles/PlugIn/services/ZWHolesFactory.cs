using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;
using System.Text.RegularExpressions;

using ZWLibrary;
using ZwSoft.ZwCAD.Geometry;

namespace DrillingHoles
{
    public class ZWHolesFactory
    {
        Transaction _tr;
        ObjectIdCollection Items;
        public ZWHolesFactory(ObjectIdCollection items, Transaction tr)
        {
            Items = items;
            _tr = tr;
        }

        /// https://regexr.com/
        /// https://regex-generator.olafneumann.org/
        /// 
        string DESCRIPTION_PATTERN = @"[a-zA-Z]+\s+(\+|\-) [0-9]+[,.][0-9]+ m";
        ///string ID_PATTERN = @"([a-zA-Z]?)+(\s?)+(\d+)?";

        /// musi być tekst i musi być numer
        string ID_PATTERN = @"([a-zA-Z])+(\s)+(\d+)";


        ObjectIdCollection _circles;
        ObjectIdCollection Circles
        {
            get {
                if(_circles is null)
                {
                    ObjectIdCollection allcircles = new Entities(Items).Filter(typeof(Circle), _tr);
                    _circles = new ObjectIdCollection(
                         allcircles.Cast<ObjectId>()
                        .Select(id => (Circle)_tr.GetObject(id, OpenMode.ForRead))
                        .Where(circle => circle.Radius > 0.49)
                        .Select(s => s.ObjectId).ToArray()
                        );
                }
                return _circles;
            }
        }
        ObjectIdCollection _descriptions;
        public ObjectIdCollection Descriptions
        {
            get
            {
                if (_descriptions is null)
                {
                    _descriptions = SelectTexts(DESCRIPTION_PATTERN);
                }
                return _descriptions;
            }
        }

        ObjectIdCollection _ids;
        public ObjectIdCollection IDs
        {
            get
            {
                if ( _ids is null)
                {
                    _ids = SelectTexts(ID_PATTERN);
                }
                return _ids;
            }
        }

        private ObjectIdCollection SelectTexts(string pattern)
        {
            ObjectIdCollection Texts = new Entities(Items).Filter(typeof(DBText), _tr);
            return new ObjectIdCollection( 
                Texts.Cast<ObjectId>()
                .Select(id => (DBText)_tr.GetObject(id, OpenMode.ForRead))
                .Where(txt => Regex.IsMatch(txt.TextString, pattern))
                .Select(s => s.ObjectId).ToArray() );
        }

        internal List< ZWHole > AsZWHoles()
        {
            
            List<ZWHole> result = new List<ZWHole>();
            int i = 0; 
            foreach (ObjectId id in Circles)
            {
                Circle c = _tr.GetObject(id, OpenMode.ForRead) as Circle;
                ZWHole hole = new ZWHole(id);
                hole.Description = GetCorresponding(c.Center, Descriptions);
                hole.ID = GetCorresponding(c.Center, IDs);
                result.Add(hole);
            }
            
            
            return result;
        }

        private ObjectId GetCorresponding(Point3d center, ObjectIdCollection texts)
        {
            double precision = 3;
            foreach (ObjectId item in texts)
            {
                Entity ent = _tr.GetObject(item, OpenMode.ForRead) as Entity;
                Extents3d? bbox = ent.Bounds;
                Extents3d? extendedBBox = bbox?.ExpandBy(precision);
                bool isPxInBBox = extendedBBox?.Contains(center) ?? false;
                if (isPxInBBox)
                {
                    return item;
                }
            }
            return ObjectId.Null;

        }
    }
}
