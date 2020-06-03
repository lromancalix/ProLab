using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Repo = ProLab.Repository;

namespace ProLab.DataAccess
{
    public class Repository<T> : Repo.IRepository<T> where T : class
    {
        protected readonly string _connectionString;

        public Repository(string connectionString) 
        {
            SqlMapperExtensions.TableNameMapper = (type) => { return $"{type.Name}"; };
            this._connectionString = connectionString;

        }

        public async Task<bool> Delete(T entity)
        {
            using ( var con = new SqlConnection(this._connectionString) ) 
            {
                return await con.DeleteAsync(entity);
            }
        }

        public async Task<T> GetById(int id)
        {
            using (var conn = new SqlConnection(this._connectionString)) 
            {
                return await conn.GetAsync<T>(id);
            }
        }

        public async Task<IEnumerable<T>> GetList()
        {
            using (var conn = new SqlConnection(this._connectionString))
            {
                return await conn.GetAllAsync<T>();
            }
        }

        public async Task<int> Insert(T entity)
        {
            using (var conn = new SqlConnection(this._connectionString))
            {
                return  await conn.InsertAsync<T>(entity);
            }
        }

        public async Task<bool> Update(T entity)
        {
            using (var conn = new SqlConnection(this._connectionString))
            {
                return await conn.UpdateAsync<T>(entity);
            }
        }
    }
}
