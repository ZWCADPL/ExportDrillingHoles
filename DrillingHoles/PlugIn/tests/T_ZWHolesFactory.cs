using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZwSoft.ZwCAD.DatabaseServices;

using ZWTests;
using ZWLibrary;
using ZwSoft.ZwCAD.Runtime;

namespace DrillingHoles.Test
{
    class T_ZWHolesFactory : T_TestCase
    {

        public override ErrorStatus Initialize()
        {
            startTransaction();
            return base.Initialize();
        }
        public override void Evaluate()
        {
            ObjectIdCollection Items = SSGet.ByLayer("GWM-Messstelle");
            ZWHolesFactory factory = new ZWHolesFactory(Items, tr);
            List<ZWHole> zwholes = factory.AsZWHoles();
            IHoleToStingConverter printer = new HoleToCSVStingConverter();
            zwholes.ForEach(h =>
            {
                ZWPrinter.NewLine();
                ZWPrinter.Print(printer.AsString(ZWHoleToHole.Convert(h, tr)));
            }
                
            );
            
            ZWAssert.Equals(zwholes.Count, 5);

            return;
        }

        public override ErrorStatus Finalize()
        {
            tr.Commit();
            return ErrorStatus.OK;
        }
    }
}
