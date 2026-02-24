using ClosedXML.Excel;
using System.IO;

public static class ExcelExportHelper
{
    public static byte[] CreateExport(List<Diplomarbeit> daten)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Gesamtauswertung");

        // HEADER
        ws.Cell(1, 1).Value = "Platz";
        ws.Cell(1, 2).Value = "Diplomarbeit";
        ws.Cell(1, 3).Value = "Namen";
        ws.Cell(1, 4).Value = "Punkte Publikum";
        ws.Cell(1, 5).Value = "Punkte Schulvoting";
        ws.Cell(1, 6).Value = "Punkte Gesamt";

        ws.Range(1, 1, 1, 7).Style.Font.Bold = true;

        int row = 2;
        int platz = 1;

        foreach (var d in daten)
        {
            int punkteJury = 0;
            int gesamt = d.PunktePublikumsvoting + d.PunkteSchulvoting + punkteJury;

            ws.Cell(row, 1).Value = platz;
            ws.Cell(row, 2).Value = d.Name;
            ws.Cell(row, 3).Value = ""; // Namen später
            ws.Cell(row, 4).Value = d.PunktePublikumsvoting;
            ws.Cell(row, 5).Value = d.PunkteSchulvoting;
            ws.Cell(row, 6).Value = gesamt;

            row++;
            platz++;
        }

        ws.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        wb.SaveAs(ms);
        return ms.ToArray();
    }
}