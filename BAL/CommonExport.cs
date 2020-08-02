using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public static class CommonExport
    {
        
        public static void ExportToExcel(this DataTable Tbl, string filename, string ExcelFilePath, string jobonly = "")
        {
            try
            {
                if (Tbl == null || Tbl.Columns.Count == 0)
                    Log.Debug(string.Format("ExportToExcel: Null or empty input table"));
                Log.Debug("strat excel workbook created ");
                // load excel, and create a new workbook
                ExcelPackage pck = new ExcelPackage();
                var workSheet = pck.Workbook.Worksheets.Add(filename);
                // column headings
                Log.Debug("excel workbook created ");
                int torow = Tbl.Rows.Count;
                int tocolumn = Tbl.Columns.Count;
                string lastprojectcode = string.Empty;
                // rows
                int g = 0;
                var cell = workSheet.Cells[g + 1, 1];
                cell.Value = filename;
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 16;
                cell.Style.Font.UnderLine = true;
                cell.Style.Font.Name = "Segoe UI Light";
                workSheet.Cells[g + 1, 1, g + 1, 4].Merge = true;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //cell.Style.Font.al
                cell.Style.WrapText = false;
                workSheet.Row((g + 1)).Height = 25;
                cell.Style.Font.Color.SetColor(Color.Purple);
                g++;


                for (int j = 0; j < Tbl.Columns.Count; j++)
                {

                    string c = Tbl.Columns[j].ToString();
                    workSheet.Cells[(g + 1), (j + 1)].Value = c;
                    workSheet.Column(j + 2).Width = 27;
                }
                cell = workSheet.Cells[(g + 1), 1, (g + 1), tocolumn];

                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.Purple);
                cell.Style.Font.Color.SetColor(Color.White);
                cell.Style.Font.Size = 9;
                cell.Style.Font.Name = "Segoe UI Light";
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Style.WrapText = false;
                cell.Style.Font.Bold = true;

                g++;
                for (int i = 0; i < Tbl.Rows.Count; i++)
                {


                    // to do: format datetime values before printing


                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {

                        string v = Tbl.Rows[i][j].ToString();
                        string c = Tbl.Columns[j].ToString();
                        if (!string.IsNullOrEmpty(c) && !string.IsNullOrEmpty(v) && v.Contains("1753") && c.Contains("Date"))
                        {
                            v = string.Empty;
                        }
                        workSheet.Cells[(g + 1), (j + 1)].Value = v;
                        cell = workSheet.Cells[(g + 1), (j + 1)];
                        workSheet.Column(j + 2).Width = 27;
                        cell.Style.Font.Size = 9;
                        cell.Style.Font.Name = "Segoe UI Light";
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        cell.Style.WrapText = false;
                    }
                    g++;


                }
                Log.Debug(string.Format("excel genrated for path {0}", ExcelFilePath));
                // check fielpath
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        DirectoryInfo dr = new DirectoryInfo(ExcelFilePath);
                        var fi = new FileInfo(dr.FullName);
                        pck.SaveAs(fi);
                        // System.Threading.Thread.Sleep(10000);
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(string.Format("ExportToExcel: Excel file could not be saved! Check filepath.\n -{0}", ex.Message.ToString()));
                    }
                }
                else    // no filepath is given
                {

                }
            }
            catch (Exception ex)
            {

                Log.Debug(string.Format("ExportToExcel -{0}", ex.Message.ToString()));
            }
        }

    }
}
