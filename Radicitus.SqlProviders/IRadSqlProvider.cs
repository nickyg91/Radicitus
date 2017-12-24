using Radicitus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Radicitus.SqlProviders
{
    public interface IRadSqlProvider
    {
        Task<Grid> InsertGridAsync(Grid grid);
        Task<IEnumerable<RadGridNumber>> InsertRadGridNumbersAsync(IEnumerable<RadGridNumber> radGridNumbers);
        Task<int> GetTotalCostAmountForGridIdAsync(int gridId);
        Task<IEnumerable<RadGridNumber>> GetMemberNumbersForGridIdAsync(int gridId);
        Task<Grid> GetGridByGridIdAsync(int gridId);
        Task<IEnumerable<Grid>> GetAllGridsAsync();
    }
}
