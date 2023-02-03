using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.ApplicationServices;

using AcadAp = ZwSoft.ZwCAD.ApplicationServices.Application;

using ZWLibrary;

namespace ZWTests
{
    public class T_TestCase
    {
        internal ObjectIdCollection ids = new ObjectIdCollection();

        public virtual ErrorStatus Initialize()
        {
            return ErrorStatus.OK;
        }

        public virtual void Evaluate()
        {
            Print(this);
            return ;
        }
        public virtual ErrorStatus Finalize()
        {
            return ErrorStatus.OK;
        }

        public void Print ( object obj)
        {
            ZWPrinter.Print(obj.ToString());
        }


        protected Transaction tr;
        protected void startTransaction()
        {
            Document Doc = AcadAp.DocumentManager.MdiActiveDocument;
            ZwSoft.ZwCAD.DatabaseServices.TransactionManager tm = Doc.Database.TransactionManager;
            tr = tm.StartTransaction();
        }

        protected void EraseDrawn()
        {
            new Entities(ids).Erase(tr);
        }

        protected void Add(Entity ent)
        {
            Database db = HostApplicationServices.WorkingDatabase;
            BlockTableRecord _space = tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
            _space.AppendEntity(ent);
            tr.AddNewlyCreatedDBObject(ent, true);

        }
    }
}
