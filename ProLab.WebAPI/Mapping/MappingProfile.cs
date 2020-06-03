using AutoMapper;
using M = ProLab.Model;

namespace ProLab.WebAPI.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Model.Request.UsuarioRequest, Model.Entidades.USER_LAB>();
        }

      

    }
}
