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
        Task<DataTable> UpdateDataTable(DataTable dataTable, List<int> originIndex, List<int> destinationIndex, bool skipFirstRow);
        Task<DataTable> GetFileAsDataTable(string filePath);
        Task SaveDataTable(string filePath, DataTable dataTable);
    }
}
