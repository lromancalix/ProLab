using System;
using System.Collections.Generic;
using System.Text;

namespace ProLab.Model.Request
{
    public class UsuarioRequest
    {
        public int id { get; set; }
        public string correo { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public int idRol { get; set; }
        public int usuarioCreacion { get; set; }
    }
}
