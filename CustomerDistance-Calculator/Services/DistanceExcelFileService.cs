using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CustomerDistance_Calculator.DTOs;
using CustomerDistance_Calculator.Utils;

namespace CustomerDistance_Calculator.Services
{
    public class DistanceExcelFileService(IDistanceService _distanceService) : IDistanceFileService
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

        public async Task<DataTable> UpdateDataTable(DataTable dataTable, int originIndex, int destinationIndex, bool skipFirstRow)
        {
            int newIndex = dataTable.Columns.Count;
            dataTable.Columns.Add($"Spalte {newIndex + 1}");
            dataTable.Columns.Add($"Spalte {newIndex + 2}");

            if(skipFirstRow && dataTable.Rows.Count > 0)
            {
                dataTable.Rows[0][newIndex] = "Entfernung in Kilometer";
                dataTable.Rows[0][newIndex + 1] = "Dauer in Minuten";
            }

            for (int row = skipFirstRow ? 1 : 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];
                string originAddress = (string)dataRow[originIndex - 1];
                string destinationAddress = (string)dataRow[destinationIndex - 1];
                try
                {
                    DistanceDto distance = await _distanceService.GetDistance(originAddress, destinationAddress);
                    dataRow[newIndex] = (distance.DistanceInMeters / 1000).ToString("0.##");
                    dataRow[newIndex + 1] = (distance.DurationInSeconds / 60).ToString("0.##");
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show($"Ein Fehler ist aufgetreten! Zeile: {row + 1} (Origin: {originAddress} | Destination: {destinationAddress})\nSoll die Verarbeitung trotzdem durchgeführt werden?", "Fehler", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        throw new Exception(ex.Message);
                    continue;
                }

            }
            return dataTable;
        }
    }
}
