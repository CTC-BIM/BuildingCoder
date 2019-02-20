﻿#region Header
//
// CmdExportIfc.cs - Export current view to IFC
//
// Copyright (C) 2018-2019 by Jeremy Tammik, Autodesk Inc. All rights reserved.
//
// Keywords: The Building Coder Revit API C# .NET add-in.
//
#endregion // Header

#region Namespaces
using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion // Namespaces

namespace BuildingCoder
{
  [Transaction( TransactionMode.Manual )]
  class CmdExportIfc : IExternalCommand
  {
    /// <summary>
    /// Export current view to IFC for
    /// https://forums.autodesk.com/t5/revit-api-forum/ifc-export-using-document-export-not-working/m-p/8118082
    /// </summary>
    static Result ExportToIfc( Document doc )
    {
      Result r = Result.Failed;

      using( Transaction tx = new Transaction( doc ) )
      {
        tx.Start( "Export IFC" );

        string desktop_path = Environment.GetFolderPath(
          Environment.SpecialFolder.Desktop );

        IFCExportOptions opt = null;

        doc.Export( desktop_path, doc.Title, opt );

        tx.RollBack();

        r = Result.Succeeded;
      }
      return r;
    }

    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      Document doc = uidoc.Document;

      ExportToIfc( doc );

      return Result.Succeeded;
    }
  }
}
