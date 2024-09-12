using CustomerDistance_Calculator.DTOs;
using CustomerDistance_Calculator.Factorys;
using CustomerDistance_Calculator.Utils;
using System.Data;
using System.Text;
using System.Windows;

namespace CustomerDistance_Calculator.Services
{
    public class DistanceExcelFileService(IFactory _factory) : IDistanceFileService
    {
        public Task<DataTable> GetFileAsDataTable(string filePath)
        {
            return Task.FromResult(ExcelUtil.GetFileAsDataTable(filePath));
        }

        public Task SaveDataTable(string filePath, DataTable dataTable)
        {
            ExcelUtil.ExportDataSetToExcel(dataTable, filePath);
            return Task.CompletedTask;
        }

        public async Task<DataTable> UpdateDataTable(DataTable dataTable, List<int> originIndex, List<int> destinationIndex, bool skipFirstRow)
        {
            int newIndex = dataTable.Columns.Count;
            dataTable.Columns.Add($"Spalte {newIndex + 1}");
            dataTable.Columns.Add($"Spalte {newIndex + 2}");

            if (skipFirstRow && dataTable.Rows.Count > 0)
            {
                dataTable.Rows[0][newIndex] = "Entfernung in Kilometer";
                dataTable.Rows[0][newIndex + 1] = "Dauer in Minuten";
            }
            List<string> errors = [];
            for (int row = skipFirstRow ? 1 : 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];
                string originAddress = GetColumnLetter(dataRow, originIndex);
                string destinationAddress = GetColumnLetter(dataRow, destinationIndex);
                try
                {
                    DistanceDto distance = await _factory.DistanceService.GetDistance(originAddress, destinationAddress);
                    dataRow[newIndex] = (distance.DistanceInMeters / 1000).ToString("0.##");
                    dataRow[newIndex + 1] = (distance.DurationInSeconds / 60).ToString("0.##");
                }
                catch (Exception)
                {
                    errors.Add("Zeile: " + (row + 1) + " (Origin: " + originAddress + " | Destination: " + destinationAddress + ")");
                }

            }
            if (errors.Count > 0 && MessageBox.Show($"Es sind {errors.Count} Fehler augetreten! Soll die Verarbeitung abgebrochen werden?  Die folgenden Zeilen konnten nicht verarbeitet werden: \n - {string.Join("\n - ", errors)}", "Fehler augetreten", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                throw new Exception();
            return dataTable;
        }

        private static string GetColumnLetter(DataRow dataRow, List<int> columns)
        {
            StringBuilder sb = new();
            foreach (int index in columns)
            {
                sb.Append(dataRow[index - 1].ToString() + " ");
            }
            string toReturn = sb.ToString();
            if (toReturn.Length > 0)
                toReturn = toReturn[..^1];
            return toReturn;
        }

    }
}
