using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrillingHoles
{
    public class DrillingTexts : Library.ITextProvider
    {
        static Dictionary<string, string> _texts;

        public DrillingTexts()
        {
            if (_texts is null)
            {
                _texts = new Dictionary<string, string>();
                _texts.Add("IDS_PromptForExcelFile", "Select file to save data");
                _texts.Add("IDS_FileFilter", "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*");
                //                _texts.Add("", "");
            }
        }
        public string get(string ID)
        {
            return _texts[ID];
        }
    }
}
