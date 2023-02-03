using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillingHoles
{
    public class HoleToCSVStingConverter : IHoleToStingConverter
    {
        string ColumnSeparator = "\t";

        IFormatProvider formater
        {
            get {                
                NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                // nfi.NumberDecimalSeparator = decSeparator;
                // nfi.NumberDecimalDigits = precision;
                // nfi.NumberGroupSeparator = "";
                return nfi;
            }
        }
        public string AsString(Hole hole)
        {            
            StringBuilder builder = new StringBuilder();
            builder.Append(hole.Position.X.ToString(formater));
            builder.Append(ColumnSeparator);
            builder.Append(hole.Position.Y.ToString(formater));
            builder.Append(ColumnSeparator);
            builder.Append(hole.ID);
            builder.Append(ColumnSeparator);
            builder.Append(hole.description);
            string result = builder.ToString();
            return result;
        }
    }
}
