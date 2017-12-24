using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Radicitus.Entities;

namespace Radicitus.SqlProviders
{
    public class RadSqlProvider : IRadSqlProvider
    {
        private readonly string _connectionString;

        public RadSqlProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Grid> InsertGridAsync(Grid grid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO ( GridName, CostPerSquare, DateCreated ) rad.Grid VALUES ( @GridName, @CostPerSquare, GETDATE() ) SELECT CAST(SCOPE_IDENTITY() AS INT)";
                var insertedGridId = await connection.ExecuteScalarAsync(sql, new { grid.GridName, grid.CostPerSquare });
                return await connection.QuerySingleAsync<Grid>(
                    "SELECT GridName, CostPerSquare, DateCreated, GridId FROM rad.Grid WHERE GridId = @GridId",
                    new {GridId = insertedGridId});
            }
        }

        public Task<IEnumerable<RadGridNumber>> InsertRadGridNumbersAsync(IEnumerable<RadGridNumber> radGridNumbers)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalCostAmountForGridIdAsync(int gridId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RadGridNumber>> GetMemberNumbersForGridIdAsync(int gridId)
        {
            throw new NotImplementedException();
        }

        public Task<Grid> GetGridByGridIdAsync(int gridId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Grid>> GetAllGridsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT GridName, GridId, DateCreated, CostPerSquare FROM rad.Grid";
                return await connection.QueryAsync<Grid>(sql);
            }
            
        }
    }
}
