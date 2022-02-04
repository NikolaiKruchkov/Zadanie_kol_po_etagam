using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Column_po_etag
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            ElementCategoryFilter columnCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Columns);
            ElementClassFilter columnInstancesFilter = new ElementClassFilter(typeof (FamilyInstance));
            int levelId1 = 311;
            int levelId2 = 6772288;
            ElementId elementId1 = new ElementId(levelId1);
            ElementId elementId2 = new ElementId(levelId2);
            ElementLevelFilter columnLevelFilter1 = new ElementLevelFilter(elementId1);
            ElementLevelFilter columnLevelFilter2 = new ElementLevelFilter(elementId2);
            List<ElementFilter> columnFilter1=new List<ElementFilter>() { columnCategoryFilter, columnInstancesFilter, columnLevelFilter1 };
            List<ElementFilter> columnFilter2 = new List<ElementFilter>() { columnCategoryFilter, columnInstancesFilter, columnLevelFilter2 };
            LogicalAndFilter columnLevFilter1 = new LogicalAndFilter(columnFilter1);
            LogicalAndFilter columnLevFilter2 = new LogicalAndFilter(columnFilter2);
            var column1 = new FilteredElementCollector(doc)
                .WherePasses(columnLevFilter1)
                .Cast<FamilyInstance>()
                .ToList();
            var column2 = new FilteredElementCollector(doc)
               .WherePasses(columnLevFilter2)
               .Cast<FamilyInstance>()
               .ToList();
            TaskDialog.Show("Columns count", $"Количество колонн на 1 этаже {column1.Count}.  Количество колонн на 2 этаже {column2.Count}");
            return Result.Succeeded;
        }
    }
}
