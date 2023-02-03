using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using System.Reflection;
using ZWLibrary;
using ZWTests;

[assembly: CommandClass(typeof(DrillingHoles.UserTest_cmd))]

namespace DrillingHoles
{
    class UserTest_cmd : T_TestSuite
    {
        ObjectIdCollection Items { get; set; }

        [CommandMethod("stt")]
        static public void Drilling_Test()
        {
            T_TestSuite tests = new UserTest_cmd();
            try
            {
                tests.Initialize();
                tests.Evaluate();
                tests.Finalize();
            }
            catch (System.Exception ex)
            {
                ZWPrinter.Print( ex.Message);
            }
        }


        public override ErrorStatus Initialize()
        {
            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "DrillingHoles.Test");
            for (int i = 0; i < typelist.Length; i++)
            {
                if (typelist[i] == this.GetType())
                    continue;

                _tests.Add(typelist[i]);
            }
            return ErrorStatus.OK;
        }
    }
}
