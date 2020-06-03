using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using U = ProLab.UnitOfWork;

namespace ProLab.WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly U.IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(U.IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        #region CREAR

        // POST: controller/crear
        [HttpPost("crear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GuardarUsuario(Model.Request.UsuarioRequest usuario)
        {
            try
            {

                Model.Entidades.USER_LAB entidad = this._mapper.Map<Model.Entidades.USER_LAB>(usuario);
                return Ok(await this._unitOfWork.User.Insert(entidad));

            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }
        }

        #endregion

        #region LISTAR TODOS

        // POST: controller/listar-todos
        [HttpGet("listar-todos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListarUsuariosActivos()
        {
            try
            {
                var response =  await this._unitOfWork.User.GetUsuariosActivos();
                return Ok( response );

            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }
        }

        #endregion

        #region OBTENER POR ID

        // POST: controller/obtener-por-id/id
        [HttpGet("obtener-por-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenenerPorId(int id)
        {
            try
            {
                Model.Entidades.USER_LAB response = await this._unitOfWork.User.GetById(id);
                var res = this._mapper.Map<Model.Response.UsuarioResponse>(response);
                return Ok(res);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }
        }

        #endregion

        #region MODIFICAR

        // POST: controller/actualizar
        [HttpPut("actualizar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ModificarUsuario(Model.Request.UsuarioRequest usuario)
        {
            try
            {
                if (!validaIdUsuario(usuario.id)) return BadRequest("Usuario no valido.");

                if (!await this.existeUsuario(usuario.id)) return BadRequest("Usuario no encontrado");

                Model.Entidades.USER_LAB entidad = this._mapper.Map<Model.Entidades.USER_LAB>(usuario);

                return Ok(await this._unitOfWork.User.Update(entidad));

            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }
        }

        private bool validaIdUsuario(int id) => (id <= 0) ? false : true;
        private async Task<bool> existeUsuario(int id) => (await this._unitOfWork.User.GetById(id) == null) ? false : true;

        #endregion

        #region ELIMINACIÓN LÓGICA

        // DELETE: controller/eliminar/id
        [HttpDelete("eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                if (!validaIdUsuario(id)) return BadRequest("Usuario no valido.");

                var usuario = await this._unitOfWork.User.GetById(id);

                if (usuario == null) return BadRequest("El usuario no existe.");

                usuario.IS_ACTIVE = false;

                Model.Entidades.USER_LAB entidad = this._mapper.Map<Model.Entidades.USER_LAB>(usuario);

                return Ok(await this._unitOfWork.User.Update(entidad));

            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }
        }

        #endregion

    }
}