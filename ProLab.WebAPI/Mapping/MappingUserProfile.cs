using AutoMapper;
using M = ProLab.Model;

namespace ProLab.WebAPI.Mapping
{
    public class MappingUserProfile : Profile
    {
        public MappingUserProfile()
        {
            CreateMap<M.Request.UsuarioRequest, M.Entidades.USER_LAB>()
                .ForMember(DEST => DEST.EMAIL, OPT => OPT.MapFrom(input => input.correo))
                .ForMember(DEST => DEST.FIRST_LAST_NAME, OPT => OPT.MapFrom(input => input.apellidoPaterno))
                .ForMember(DEST => DEST.SECOND_LAST_NAME, OPT => OPT.MapFrom(input => input.apellidoMaterno))
                .ForMember(DEST => DEST.FIRST_NAME, OPT => OPT.MapFrom(input => input.primerNombre))
                .ForMember(DEST => DEST.SECOND_NAME, OPT => OPT.MapFrom(input => input.segundoNombre))
                .ForMember(DEST => DEST.PASSWORD, OPT => OPT.MapFrom(input => input.contrasena))
                .ForMember(DEST => DEST.USR, OPT => OPT.MapFrom(input => input.usuario))
                .ForMember(DEST => DEST.ID_USER_ROL, OPT => OPT.MapFrom(input => input.idRol)).ReverseMap();

            CreateMap<M.Response.UsuarioResponse, M.Entidades.USER_LAB>()
                .ForMember(DEST => DEST.EMAIL, OPT => OPT.MapFrom(input => input.correo))
                .ForMember(DEST => DEST.FIRST_LAST_NAME, OPT => OPT.MapFrom(input => input.apellidoPaterno))
                .ForMember(DEST => DEST.SECOND_LAST_NAME, OPT => OPT.MapFrom(input => input.apellidoMaterno))
                .ForMember(DEST => DEST.FIRST_NAME, OPT => OPT.MapFrom(input => input.primerNombre))
                .ForMember(DEST => DEST.SECOND_NAME, OPT => OPT.MapFrom(input => input.segundoNombre))
                .ForMember(DEST => DEST.PASSWORD, OPT => OPT.MapFrom(input => input.contrasena))
                .ForMember(DEST => DEST.USR, OPT => OPT.MapFrom(input => input.usuario))
                .ForMember(DEST => DEST.ID_USER_ROL, OPT => OPT.MapFrom(input => input.idRol)).ReverseMap();



        }
    }
}
