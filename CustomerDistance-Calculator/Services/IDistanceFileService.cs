using CustomerDistance_Calculator.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.Services
{
    public interface IDistanceFileService
    {
        Task<DataTable> UpdateDataTable(DataTable dataTable, List<int> originIndex, List<int> destinationIndex, bool skipFirstRow, Action<StatusDto> status);
        Task<DataTable> GetFileAsDataTable(string filePath, Action<StatusDto> status);
        Task SaveDataTable(string filePath, DataTable dataTable, Action<StatusDto> status);
    }
}
