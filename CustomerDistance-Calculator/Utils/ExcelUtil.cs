using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace CustomerDistance_Calculator.Utils
{
    public static class ExcelUtil
    {
        public static DataTable GetFileAsDataTable(string filePath)
        {
            DataTable dataTable = new();

            Application excel = new();
            Workbook workbook = excel.Workbooks.Open(filePath);
            Worksheet worksheet = workbook.ActiveSheet as Worksheet;
            try
            {
                Range excelRange = worksheet.UsedRange;
                int rowCount = excelRange.Rows.Count;
                int colCount = excelRange.Columns.Count;

                for (int col = 1; col <= colCount; col++)
                    dataTable.Columns.Add($"Spalte {col}");

                for (int row = 1; row <= rowCount; row++)
                {
                    DataRow dataRow = dataTable.NewRow();

                    for (int col = 1; col <= colCount; col++)
                    {
                        string cellValue = (excelRange.Cells[row, col] as Range).Text;
                        dataRow[col - 1] = cellValue;
                    }
                    throw new ArgumentNullException("dfsf");
                    dataTable.Rows.Add(dataRow);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Fehler beim Einlesen der Excel-Datei: {ex.Message}");
            }
            finally
            {
                workbook.Close(false);
                excel.Quit();

                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excel);
            }
            return dataTable;
        }

        public static void ExportDataSetToExcel(DataTable dataTable, string filePath)
        {
            Application excelApp = new();

            try
            {
                Workbook workbook = excelApp.Workbooks.Add();
                Worksheet worksheet = (Worksheet)workbook.Sheets[1];

                int row = 1;
                worksheet.Cells[row++, 1] = dataTable.TableName;

                for (int col = 1; col <= dataTable.Columns.Count; col++)
                {
                    worksheet.Cells[row, col] = dataTable.Columns[col - 1].ColumnName;
                }
                row++;

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    for (int col = 1; col <= dataTable.Columns.Count; col++)
                    {
                        string cellValue = dataRow[col - 1].ToString() ?? "";
                        worksheet.Cells[row, col] = cellValue;
                    }
                    row++;
                }
                workbook.SaveAs(filePath);
                workbook.Close();
            }
            finally
            {
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
            }
        }
    }
}
