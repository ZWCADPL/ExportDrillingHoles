using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ZwSoft.ZwCAD.Runtime;

namespace ZWTests
{
    public class ZWAssert
    {

        public static bool True(bool condition)
        {        
            if ( condition == true ) return true ;
            throw new System.Exception(Environment.NewLine + "condition is: false " );
        }

        public static void False(bool condition, ErrorStatus es)
        {
            if (condition == false) return ;
            throw new System.Exception(es.ToString());
        }

        public static bool False(bool condition)
        {
            if (condition == false) return true;
            throw new System.Exception(Environment.NewLine + "condition is: true");
        }

        public static bool IsOk(ErrorStatus es)
        {
            if ( es == ErrorStatus.OK)
                return true;
            throw new System.Exception(Environment.NewLine + "result is: " + es.ToString() + " while expected is: " + ErrorStatus.OK);

        }

        new public static bool Equals( object val , object expected)
        {
            if ( val.Equals( expected ) )
                return true;
            throw new System.Exception(Environment.NewLine + "result is: " + val.ToString() + " while expected is: " + expected.ToString() );
        }

        public static void Error(ErrorStatus es)
        {
            if (es == ErrorStatus.OK)
                return;
            throw new System.Exception(Environment.NewLine + "result is: " + es.ToString() );

        }

        public static void NotNull(object obj)
        {
            if (obj == null)
                throw new System.ArgumentNullException();
        }

        public static void IsEmptyString(string result)
        {
            if ( ! string.IsNullOrEmpty(result) )
                throw new System.Exception(Environment.NewLine + "result is: " + result + " while expected is empty string");
        }

        public static void True(bool v, ErrorStatus es)
        {
            if (es == ErrorStatus.OK)
                return;
            throw new System.Exception(Environment.NewLine + es.ToString() );

        }

        public static void Equals(object result, object expected, ErrorStatus es)
        {
            if (result == expected)
                return;
            throw new System.Exception(Environment.NewLine + es.ToString());
        }
    }
}
