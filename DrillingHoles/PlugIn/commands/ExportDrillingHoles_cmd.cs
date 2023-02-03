using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using AcadAp = ZwSoft.ZwCAD.ApplicationServices.Application;

using System.Windows.Forms;


using Library;
using ZWLibrary;

[assembly: CommandClass(typeof(DrillingHoles.ExportDrillingHoles_cmd))]

namespace DrillingHoles
{
    // public event EventHandler<ServerEventArgs> EntitiesSelected;
    // public event EventHandler<ServerEventArgs> TargetpathSelected;

    /// to może rzucać model;    
    // public event EventHandler<ServerEventArgs> HolesExported;

    class ExportDrillingHoles_cmd
    {
        public ExportDrillingHoles_cmd()
        {
            _texts = new DrillingTexts();
        }
        ObjectIdCollection Items { get; set; }
        string Localization { get; set; }

        Library.ITextProvider _texts;
        Transaction _tr;

        [CommandMethod("ExportDrillingHoles")]
        static public void ExportDrillingHoles()
        {
            ExportDrillingHoles_cmd cmd = new ExportDrillingHoles_cmd();
            try
            {
                cmd.Initialize();
                cmd.Evaluate();
            }
            catch (SystemException ex)
            {
                if (ex is UserBreakException)
                { }
                else
                {
                    ZWPrinter.NewLine();
                    ZWPrinter.Print(ex.Message);
                }
            }
            finally
            {
                cmd.Finalize();
            }
        }

        private void Evaluate()
        {
            HolesRepository dwgrepo = new DWG_HolesRepository(Items, _tr);
            HolesRepository repo = new CSV_HolesRepository(Localization);
            repo.Add(dwgrepo.Holes);
            repo.Save();
        }

        private void Initialize()
        {
            startUndoMark();
            startTransaction();
            askForEntities();
            askForLocalization();
        }
        protected void startTransaction()
        {
            Document Doc = AcadAp.DocumentManager.MdiActiveDocument;
            ZwSoft.ZwCAD.DatabaseServices.TransactionManager tm = Doc.Database.TransactionManager;
            _tr = tm.StartTransaction();
        }

        private void askForEntities()
        {
            Items = SSGet.ByLayer("GWM-Messstelle");
        }

        FileDialog PathSelector
        {
            get
            {
                SaveFileDialog result = new SaveFileDialog();
                result.Filter = _texts.get("IDS_FileFilter");
                result.DefaultExt = "csv";
                result.Title = _texts.get("IDS_PromptForExcelFile");
                return result;
            }
        }

        private void askForLocalization()
        {
            FileDialog selector = PathSelector;
            DialogResult? askforFileResult = selector?.ShowDialog();
            if (askforFileResult != DialogResult.OK)
                throw new UserBreakException();
            Localization = selector.FileName;
        }


            private void Finalize()
        {
            _tr?.Commit();
            endUndoMark();
        }


        private void startUndoMark()
        {
            ZwSoft.ZwCAD.Internal.Utils.SetUndoMark(true);

        }

        private void endUndoMark()
        {
            ZwSoft.ZwCAD.Internal.Utils.SetUndoMark(false);
        }
    }
}
