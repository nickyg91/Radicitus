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
                const string sql = "INSERT INTO rad.Grid ( GridName, CostPerSquare, DateCreated ) VALUES ( @GridName, @CostPerSquare, GETDATE() ) SELECT CAST(SCOPE_IDENTITY() AS INT)";
                var insertedGridId = await connection.ExecuteScalarAsync(sql, new { grid.GridName, grid.CostPerSquare }).ConfigureAwait(false);
                return await connection.QuerySingleAsync<Grid>(
                    "SELECT GridName, CostPerSquare, DateCreated, GridId FROM rad.Grid WHERE GridId = @GridId",
                    new {GridId = insertedGridId}).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<RadGridNumber>> InsertRadGridNumbersAsync(IEnumerable<RadGridNumber> radGridNumbers)
        {
            var insertedGridNumbers = new List<RadGridNumber>();
            //doing the naieve way for now until i feel less lazy to make the TVP interfaces.
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    const string insertSql =
                        "INSERT INTO rad.RadGridNumber ( GridId, GridNumber, RadMemberName, HasPaid ) VALUES ( @GridId, @GridNumber, @RadMemberName, @HasPaid ) SELECT CAST(SCOPE_IDENTITY() AS INT)";
                    const string selectBackInsertedRecord =
                        "SELECT RadNumberId, GridId, RadMemberName, HasPaid FROM rad.RadGridNumber WHERE RadNumberId = @InsertedId";
                    try
                    {
                        foreach (var radNumber in radGridNumbers)
                        {
                            var insertedId = await connection.ExecuteAsync(insertSql,
                                new
                                {
                                    radNumber.GridId,
                                    radNumber.GridNumber,
                                    radNumber.RadMemberName,
                                    radNumber.HasPaid
                                });
                            var insertedRecord =
                                await connection.QuerySingleAsync<RadGridNumber>(selectBackInsertedRecord,
                                    new {InsertedId = insertedId});
                            insertedGridNumbers.Add(insertedRecord);
                        }
                        transaction.Commit();
                        return insertedGridNumbers;
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<int> GetTotalCostAmountForGridIdAsync(int gridId, int gridSquareCost)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT COUNT(*) FROM rad.RadGridNumber WHERE GridId = @GridId";
                return await connection.ExecuteScalarAsync<int>(sql, new { GridId = gridId }) * gridSquareCost; 
            }
        }

        public async Task<IEnumerable<RadGridNumber>> GetMemberNumbersForGridIdAsync(int gridId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql =
                    "SELECT RadNumberId, GridId, GridNumber, RadNemberName, HasPaid FROM rad.RadGridNumber WHERE GridId = @GridId";
                return await connection.QueryAsync<RadGridNumber>(sql, new { GridId = gridId });
            }
        }

        public async Task<Grid> GetGridByGridIdAsync(int gridId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql =
                    "SELECT GridName, GridId, DateCreated, CostPerSquare FROM rad.Grid WHERE GridId = @GridId";
                return await connection.QuerySingleAsync<Grid>(sql, new {GridId = gridId});
            }
        }

        public async Task<IEnumerable<Grid>> GetAllGridsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT TOP 100 GridName, GridId, DateCreated, CostPerSquare FROM rad.Grid ORDER BY DateCreated DESC";
                return await connection.QueryAsync<Grid>(sql).ConfigureAwait(false);
            }
            
        }

        public Task<HashSet<int>> GetAllUsedNumbersForGridAsync(int gridId)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<int, RadGridNumber>> GetMemberNumbersForGridAsynv(int gridId)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertMemberNumbersAsync(List<RadGridNumber> numbers)
        {
            throw new NotImplementedException();
        }
    }
}
