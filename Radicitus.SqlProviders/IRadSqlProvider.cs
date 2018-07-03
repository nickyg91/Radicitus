using Radicitus.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Radicitus.SqlProviders
{
    public interface IRadSqlProvider
    {
        Task<Grid> InsertGridAsync(Grid grid);
        Task<IEnumerable<RadGridNumber>> InsertRadGridNumbersAsync(IEnumerable<RadGridNumber> radGridNumbers);
        Task<int> GetTotalCostAmountForGridIdAsync(int gridId, int gridSquareCost);
        Task<IEnumerable<RadGridNumber>> GetMemberNumbersForGridIdAsync(int gridId);
        Task<Grid> GetGridByGridIdAsync(int gridId);
        Task<IEnumerable<Grid>> GetAllGridsAsync();
        Task<HashSet<int>> GetAllUsedNumbersForGridAsync(int gridId);
        Task<Dictionary<int, RadGridNumber>> GetMemberNumbersForGridAsync(int gridId);
        Task<RadGridNumber> DrawWinner(int gridId);
        Task<bool> AuthenticateUser(string username, byte[] password);
        Task<int> CreateNewsFeed(NewsFeed feed);
        Task<List<NewsFeed>> GetLastTenFeeds();
    }
}
