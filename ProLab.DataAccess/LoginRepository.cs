using ProLab.Model.Entidades;
using ProLab.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProLab.DataAccess
{
    public class LoginRepository :  ILoginRepository
    {
        private readonly string _connectionString;
        private SPExecute.IProcedureParameter _spParameter;

        public LoginRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task<AUTENTICA> ValidaUsuario(string usuario, string contrasena)
        {
            this.SetParameter(usuario, contrasena);

            IEnumerable<AUTENTICA> response = await this.GetData();

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

        private async Task<IEnumerable<AUTENTICA>> GetData() =>
            await new SPExecute.ExecuteProcedure<AUTENTICA>(this._spParameter).ExecuteSP();

    }
}
