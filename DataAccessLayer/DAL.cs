using Microsoft.Extensions.Configuration;
using CoreLib;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
     public  class DAL : Disposer
    {
        private readonly string _connection;
        public DynamicParameters _params { get; set; }
        public DAL(string connectionString)
        {
            _connection = connectionString;
        }

        public async Task<IEnumerable<T>> ExecQuery<T>(string  query)
        {
            using (var DB = new SqlConnection(_connection))
            {
                return await DB.QueryAsync<T>(query, _params);
            }
        }

        public async Task<int> ExecNonQuery(string query)
        { 
            using (IDbConnection DB = new SqlConnection(_connection))
            {
                return await DB.ExecuteAsync(query, _params);
            }
        }

        public async Task<T> ExecScalar<T>(string query)
        {
            using (IDbConnection DB = new SqlConnection(_connection))
            {
                return await DB.ExecuteScalarAsync<T>(query, _params);
            }
        }
    }
}
