using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ProLab.DataAccess.SPExecute
{
    internal class ExecuteProcedure<T> where T : class
    {
        IProcedureParameter _spParameter;
        public ExecuteProcedure(IProcedureParameter spParameter)
        {
            this._spParameter = spParameter;
        }

        public async Task<IEnumerable<T>> ExecuteSP() 
        {
            using (var conn = new SqlConnection(this._spParameter.connectionString)) 
            {
                return await conn.QueryAsync<T>(
                    this._spParameter.SPName,
                    this._spParameter.SPPatameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
        }

    }
}
