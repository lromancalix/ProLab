using ProLab.Model;
using ProLab.Model.Entidades;
using ProLab.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using M = ProLab.Model;
using R = ProLab.Repository;


namespace ProLab.DataAccess
{
    public class UserRepository : Repository<M.Entidades.USER_LAB>, R.IUserRepository
    {
        private SPExecute.IProcedureParameter _spParameter;
        public UserRepository(string connectionString): base(connectionString)
        {
            
        }

        #region USUARIOS ACTIVOS.
        /// <summary>
        /// Listado de usuarios activos.
        /// </summary>
        /// <returns></returns>
        public async Task<List<USER_LAB>> GetUsuariosActivos()
        {
            var response = await this.GetList();

            return response.ToList().Where(U => U.IS_ACTIVE).ToList();
        }

        #endregion


    }
}
