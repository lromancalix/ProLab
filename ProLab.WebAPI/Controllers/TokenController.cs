using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using U = ProLab.UnitOfWork;
using ProLab.WebAPI.Authentication;
using ProLab.Model.Entidades;

namespace ProLab.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly U.IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TokenController(U.IUnitOfWork unitOfWork, IMapper mapper, ITokenProvider tokenProvider)
        {
            this._tokenProvider = tokenProvider;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        // POST: controller/login
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Login(Model.Request.Login usuario)
        {
            try
            {
                var lista = await this._unitOfWork.User.GetList();
                var user = await this._unitOfWork.Login.ValidaUsuario(usuario.user, usuario.password);
                
                if (user == null) return Unauthorized("Usuario o contraseña incorrecto .");

                

                var token = new JsonWebToken() 
                {
                    Access_Token = this._tokenProvider.CreateToken( user, DateTime.UtcNow.AddHours(8) ),
                    Token_Type = "bearer",
                    Expires_in = 480 //minute
                };

                return Ok(token);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message.ToString());
            }
        }

    }
} 