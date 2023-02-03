using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.Runtime;

namespace ZWLibrary
{
    public class Entities
    {
        ObjectIdCollection _ids = new ObjectIdCollection();
        public Entities ( ObjectIdCollection ids)
        {
            foreach (ObjectId id in ids)
            {
                _ids.Add(id);
            }            
        }

        public Entities(BlockTableRecordEnumerator items)
        {
            while (items.MoveNext())
            {
                _ids.Add((ObjectId)items.Current);
            }
        }


        public void TransformBy(Matrix3d transMatrix, Transaction tr)
        {
            foreach (ObjectId id in _ids)
            {
                Entity ent = tr.GetObject(id, OpenMode.ForWrite, false) as Entity;
                if (ent != null)
                    ent.TransformBy(transMatrix);
            }
        }

        public void Erase()
        {
             Document doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

             using (DocumentLock docLock = doc.LockDocument())
             {
                 try
                 {
                    using (Transaction tr = doc.TransactionManager.StartTransaction())
                    {
                        Erase(tr);
                         tr.Commit();
                    }                         
                 }
                 catch (System.Exception ex)
                 {
                     ZWPrinter.Print(ex);
                 }
             }
        }

        public void Erase(Transaction tr)
        {
            foreach (ObjectId id in _ids)
            {
                Entity ent = tr.GetObject(id, OpenMode.ForWrite, false) as Entity;
                if (ent == null)
                    continue;
                ent.Erase();
            }
        }

        public void SetColor (int colorindex , Transaction tr)
        {
            foreach (ObjectId id in _ids)
            {
                Entity ent = tr.GetObject(id, OpenMode.ForWrite, false) as Entity;
                if (ent == null)
                    continue;
                ent.ColorIndex = colorindex;
            }
        }
        public ObjectIdCollection Filter(Type entititype, Transaction tr)
        {
            IEnumerable<ObjectId> selected = _ids.Cast<ObjectId>()
                .Select(id => (Entity)tr.GetObject(id, OpenMode.ForRead))
                .Where(ent => ent.GetType() == entititype)
                .Select(s => s.ObjectId);

            ObjectIdCollection result = new ObjectIdCollection( selected.ToArray() );
            // foreach (ObjectId id in _ids)
            // {
            //     DBObject asObj = tr.GetObject(id, OpenMode.ForRead);
            //     if ( asObj is entititype)
            //     {
            //         result.Add(id);
            //     }
            // 
            // }
            return result;
        }
    }
}
