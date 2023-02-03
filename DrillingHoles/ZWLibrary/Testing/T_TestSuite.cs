using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using ZwSoft.ZwCAD.Runtime;

namespace ZWTests
{
    public class T_TestSuite : T_TestCase
    {
        protected Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        protected List<Type> _tests = new List<Type>();

        public override ErrorStatus Initialize()
        {
            return ErrorStatus.OK;
        }

        public override void Evaluate()
        {
            foreach (Type testCase in _tests)
            {
                T_TestCase test = Activator.CreateInstance(testCase) as T_TestCase;
                try
                {
                    test.Initialize();
                    test.Evaluate();
                    Print(".");
                }
                catch (System.Exception es)
                {
                    Print(Environment.NewLine + es.Message + " in test: " + test.GetType().Name + Environment.NewLine);
                }
                try
                {
                    test.Finalize();
                }
                catch (System.Exception es )
                {
                    Print(Environment.NewLine + " cleanUp error: " + es.Message + " in test: " + test.GetType().Name + Environment.NewLine);
                }

            }
            return;
        }
        public override ErrorStatus Finalize()
        {
            Print(Environment.NewLine + "- " + GetType().Name + Environment.NewLine);
            return ErrorStatus.OK;
        }
    }
}
