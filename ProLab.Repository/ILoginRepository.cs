using ProLab.Model.Entidades;
using System.Threading.Tasks;

namespace ProLab.Repository
{
    public interface ILoginRepository 
    {
        Task<AUTENTICA> ValidaUsuario(string usuario, string contrasena);
    }
}
