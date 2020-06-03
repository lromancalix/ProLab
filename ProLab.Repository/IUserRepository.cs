using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using M = ProLab.Model;

namespace ProLab.Repository
{
    public interface IUserRepository : IRepository<M.Entidades.USER_LAB>
    {
        Task<M.Entidades.USER_LAB> ValidaUsuario(string usuario, string contrasena);
        Task<List<M.Entidades.USER_LAB>> GetUsuariosActivos();
    }
}
