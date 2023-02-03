using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.EditorInput;

namespace ZWLibrary
{
    public class SSGet
    {
        string _prompt;
        PromptSelectionResult _selset;

        public SSGet()
        {
            _prompt = "";
        }

        public SSGet( string prompt )
        {
            _prompt = prompt;
        }

        public static ObjectIdCollection GetActive()
        {
            return new SSGet().getActive();
        }

        public ObjectIdCollection getActive()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            _selset = acDoc.Editor.SelectImplied();
            if (_selset == null) { return null; } 
            if (_selset.Value == null) { return null; } 
            return new ObjectIdCollection(_selset.Value.GetObjectIds());
        }

        public static ObjectIdCollection Select( string prompt)
        {
            SSGet selector = new SSGet(prompt);
            selector.select();
            if (selector._selset.Status == PromptStatus.OK)
            {
                return new ObjectIdCollection(selector._selset.Value.GetObjectIds());
            }
            return null;
        }

        public static ObjectIdCollection ByLayer(string layer)
        {
            return ByFilter(DxfCode.LayerName, layer);
        }

        public static ObjectIdCollection ByXData(string xname)
        {
            return ByFilter(DxfCode.ExtendedDataRegAppName, xname);
        }

        public static ObjectIdCollection ByFilter(DxfCode code , string value)
        {
            TypedValue[] tvs = new TypedValue[]
                {
                     new TypedValue((int)code,  value ),
                };
            SelectionFilter filter = new SelectionFilter(tvs);
            return ByFilter(filter);
        }

        public static ObjectIdCollection ByFilter(SelectionFilter filter)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptSelectionResult _selset = ed.SelectAll(filter);
            if (_selset.Value is null)
                return null;

            ObjectIdCollection result = new ObjectIdCollection(_selset.Value.GetObjectIds());
            return result;
        }

        private void select()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            PromptSelectionOptions opts = new PromptSelectionOptions();
            opts.MessageForAdding = _prompt;
            _selset = acDoc.Editor.GetSelection(opts);
        }

        public static void SetSelected(ObjectIdCollection selItems)
        {
            try
            {
                if (selItems.Count == 0)
                {
                    return;
                }
                Document acDoc = Application.DocumentManager.MdiActiveDocument;
                ObjectId[] ids = new ObjectId[selItems.Count];
                selItems.CopyTo(ids, 0);
                acDoc.Editor.SetImpliedSelection(ids);
            }
            catch (System.Exception ex)
            {
                ZWPrinter.Print(ex);
            }
        }

        public static void ClearSelection()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            ObjectId[] ids = new ObjectId[0];
            acDoc.Editor.SetImpliedSelection(ids);
        }

        public static ObjectIdCollection All()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptSelectionResult _selset = ed.SelectAll();
            if (_selset.Status == PromptStatus.Error)
            {
                return null;
            }
            ObjectIdCollection result = new ObjectIdCollection(_selset.Value.GetObjectIds());

            return result;
        }

        public static ObjectIdCollection ByCrossingPoligon( Point3dCollection Points)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptSelectionResult _selset = ed.SelectCrossingPolygon(Points);
            if (_selset.Status != PromptStatus.OK)
                return null;
           /// zaznacza tylko te które są w pełni wewnątrz.
            // PromptSelectionResult _selset = ed.SelectWindowPolygon(Points);
            ObjectIdCollection result = new ObjectIdCollection(_selset.Value.GetObjectIds());
            return result;
        }

        public static ObjectIdCollection ByInsidePoligon(Point3dCollection Points)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            
            /// zaznacza tylko te które są w pełni wewnątrz.
            PromptSelectionResult _selset = ed.SelectWindowPolygon(Points);
            if (_selset.Status != PromptStatus.OK)
                return null;
            ObjectIdCollection result = new ObjectIdCollection(_selset.Value.GetObjectIds());
            return result;
        }

        public static ObjectIdCollection ByWindow(Extents3d bbox)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            // PromptSelectionResult _selset = ed.SelectCrossingPolygon(Points);

            //PromptSelectionResult _selset = ed.SelectWindow(bbox.MinPoint , bbox.MaxPoint);

            /// zaznacza ;
            PromptSelectionResult _selset = ed.SelectCrossingWindow(bbox.MinPoint, bbox.MaxPoint);

            ObjectIdCollection result = new ObjectIdCollection(_selset.Value.GetObjectIds());
            return result;
        }

        /// <summary>
        ///  trzeba odfiltrować bloki
        /// </summary>
        /// <returns></returns>
        public PromptStatus AskForBlocks()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            TypedValue[] acTypValAr = new TypedValue[1];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);

            SelectionFilter BlocksFilter = new SelectionFilter(acTypValAr);
            _selset = acDoc.Editor.GetSelection(BlocksFilter);
            
            return _selset.Status;
        }

        public PromptStatus AskForAnyLine()
        {
            return selectByType("*LINE");
        }

        public PromptStatus AskForPolyline()
        {
            return selectByType("*POLYLINE");
        }

        private PromptStatus selectByType(String entityType)
        {

            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            TypedValue[] acTypValAr = new TypedValue[1];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, entityType), 0);

            SelectionFilter Filter = new SelectionFilter(acTypValAr);
            _selset = acDoc.Editor.GetSelection(Filter);

            return _selset.Status;
        }

        public IEnumerator GetEnumerator()
        {
            return _selset.Value.GetEnumerator();
        }

        public ObjectIdCollection Items
        {
            get
            {
                return new ObjectIdCollection(_selset.Value.GetObjectIds());
            }            
        }

        public PromptStatus AskForAnyEntities()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            _selset = acDoc.Editor.GetSelection();
            return _selset.Status;
        }
    }
}
