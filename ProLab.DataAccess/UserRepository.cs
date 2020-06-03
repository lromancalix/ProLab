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
        SPExecute.IProcedureParameter _spParameter;
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

        #region LOGIN

        public async Task<M.Entidades.USER_LAB> ValidaUsuario(string usuario, string contrasena)
        {
        
            this.SetParameter(usuario, contrasena);

            IEnumerable<M.Entidades.USER_LAB> response = await this.GetData();

            return response.FirstOrDefault();
        
        }

        private void SetParameter(string usuario, string contrasena)
        {
            this._spParameter = new SPExecute.ProcedureParameter();
            _spParameter.connectionString = this._connectionString;
            _spParameter.SPName = "ValidaUsuario";
            _spParameter.SPPatameters = new Dapper.DynamicParameters();
            _spParameter.SPPatameters.Add("@usuario", usuario);
            _spParameter.SPPatameters.Add("@contrasena", contrasena);
        }

        private async Task<IEnumerable<M.Entidades.USER_LAB>> GetData() =>
            await new SPExecute.ExecuteProcedure<M.Entidades.USER_LAB>(this._spParameter).ExecuteSP();

        #endregion

    }
}
