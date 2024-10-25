using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace CURSOREVITAPI
{
    [Transaction(TransactionMode.Manual)]
    public class CmdExternalCommand:IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region INICIANDO DOCUMENTOS

            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection select = uidoc.Selection;

            #endregion


            ICollection<ElementId> seleccionActual = select.GetElementIds();
            foreach (ElementId id in seleccionActual)
            {
                Element elemento = doc.GetElement(id);
                TaskDialog.Show("Elemento", "Elemento ID: " + id.IntegerValue);
            }
            
            


            return Result.Succeeded;
        }
    }
   
}
