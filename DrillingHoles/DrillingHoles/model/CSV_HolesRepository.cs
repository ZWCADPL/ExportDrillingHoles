using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DrillingHoles
{
    public class CSV_HolesRepository : HolesRepository
    {
        public override void Save()
        {
            string[] contents = HolesToStrings();
            File.WriteAllLines(path, contents);
        }

        IHoleToStingConverter converter = new HoleToCSVStingConverter();
        private string path;

        public CSV_HolesRepository(string path)
        {
            this.path = path;
        }

        private string[] HolesToStrings()
        {
            List<string> result = new List<string>();
            _holes.ForEach(h =>
                result.Add(converter.AsString(h))
            );
            return result.ToArray();
        }
    }
}
