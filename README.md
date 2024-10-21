<!-- ENCABEZADOS -->
# **CURSO REVIT API BASICO**

## Este curso tiene como objetivo introducir a los estudiantes a la Revit API y enseñarles cómo desarrollar aplicaciones sencillas que interactúen con Revit. Al finalizar, los estudiantes tendrán una base sólida en los conceptos fundamentales de la API y podrán realizar tareas simples de automatización. 

# Temario 
1. INTRODUCCION A LA REVIT API
    # - ¿Qué es una API
    Las API son mecanismos que permiten a dos componentes de software comunicarse entre sí mediante un conjunto de definiciones y protocolos. Por ejemplo, el sistema de software del instituto de meteorología contiene datos meteorológicos diarios. La aplicación meteorológica de su teléfono “habla” con este sistema a través de las API y le muestra las actualizaciones meteorológicas diarias en su teléfono.
    [Definicion Revit API](https://aws.amazon.com/es/what-is/api/)
---

```C#
﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;


namespace BIMCOPA.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CmdSelectDetailItem : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region 1.0_ACTIVANDO LA VISTA REVIT
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            #endregion

            #region MAIN            

            FilteredElementCollector coleccion = new FilteredElementCollector(doc, doc.ActiveView.Id);
            ElementClassFilter filtrodeclase = new ElementClassFilter(typeof(FamilyInstance));
            ElementCategoryFilter filtrocategoria = new ElementCategoryFilter(BuiltInCategory.OST_DetailComponents);
            LogicalAndFilter filtro = new LogicalAndFilter(filtrodeclase, filtrocategoria);            

            FilteredElementCollector coleccion2 = new FilteredElementCollector(doc, doc.ActiveView.Id);
            ElementClassFilter filtrodeclase2 = new ElementClassFilter(typeof(FamilyInstance));
            ElementCategoryFilter filtrocategoria2 = new ElementCategoryFilter(BuiltInCategory.OST_GenericAnnotation);
            LogicalAndFilter filtro2 = new LogicalAndFilter(filtrodeclase2, filtrocategoria2);

            FilteredElementCollector TextNotes = new FilteredElementCollector(doc, doc.ActiveView.Id);
            List<Element> TextNotesSelection = TextNotes.OfCategory(BuiltInCategory.OST_TextNotes).WhereElementIsNotElementType().ToList();
            List<ElementId> ElementosTextNotes = TextNotesSelection.Select(e => e.Id).ToList();

            FilteredElementCollector DetailTags = new FilteredElementCollector(doc, doc.ActiveView.Id);
            List<Element> DetailTagsSelection = DetailTags.OfCategory(BuiltInCategory.OST_DetailComponentTags).WhereElementIsNotElementType().ToList();
            List<ElementId> ElementosDetailTags = DetailTagsSelection.Select(e => e.Id).ToList();

           

            ICollection<ElementId> elementosSeleccionados = coleccion.WherePasses(filtro).ToElementIds();                               // MODIFICAR LA LISTA POR UNA iCOLECCTION QUE RECIBA PARAMETROS ID 
            ICollection<ElementId> elementosSeleccionados2 = coleccion2.WherePasses(filtro2).ToElementIds();



            List<ElementId> elementIdsSeleccionados = new List<ElementId>();
            elementIdsSeleccionados.AddRange(elementosSeleccionados2);
            elementIdsSeleccionados.AddRange(elementosSeleccionados);
            elementIdsSeleccionados.AddRange(ElementosTextNotes);
            elementIdsSeleccionados.AddRange(ElementosDetailTags);
            //elementIdsSeleccionados.AddRange(ElementosDetailTagsWalls);

            uidoc.Selection.SetElementIds(elementIdsSeleccionados);



            #endregion

            return Result.Succeeded;
        }
    }
}


```


