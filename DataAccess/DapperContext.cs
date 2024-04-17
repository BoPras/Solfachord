using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public List<T> GetList<T>(string query, DynamicParameters parameters, CommandType ct = CommandType.Text, int timeOut = 120) 
        {
            try
            {
                using IDbConnection db = this.CreateConnection();
                return db.Query<T>(query, parameters, commandType: ct, commandTimeout: timeOut).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public T Update<T>(string query, DynamicParameters parameters, CommandType ct = CommandType.Text, int timeOut = 120)
        {
            T result;
            using IDbConnection db = this.CreateConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                using var transaction = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(query, parameters, commandType: ct, commandTimeout: timeOut, transaction: transaction).FirstOrDefault();
                    transaction.Commit();
                    return result;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    if (db.State == ConnectionState.Open)
                        db.Close();
                    throw e;
                }
            }
            catch (Exception e)
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
                throw e;
            }
        }
    }
}
