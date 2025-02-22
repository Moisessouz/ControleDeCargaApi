using ControleDeCarga.DTO.Usuario;
using ControleDeCarga.Models;
using ControleDeCarga.Services.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCarga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioInterface;
        public UsuarioController(IUsuarioService usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseEntity<UsuarioDto>>> Login(string email, string senha)
        {
            var resultado = await _usuarioInterface.Login(email, senha);

            return Ok(resultado);
        }

        [HttpGet("GetAllUsuarios")]
        public async Task<ActionResult<ResponseEntity<List<UsuarioDto>>>> GetAllUsuarios()
        {
            var usuarios = await _usuarioInterface.GetAllUsuarios();

            return Ok(usuarios);
        }

        [HttpGet("GetUsuarioById")]
        public async Task<ActionResult<ResponseEntity<UsuarioDto>>> GetUsuarioById(int usuarioId)
        {
            var usuario = await _usuarioInterface.GetUsuarioById(usuarioId);

            return Ok(usuario);
        }

        [HttpPost("CreateNewUsuario")]
        public async Task<ActionResult<ResponseEntity<CreateUsuarioDto>>> CreateNewUsuario(CreateUsuarioDto newUsuario)
        {
            var usuarioEntity = await _usuarioInterface.CreateNewUsuario(newUsuario);

            return Ok(usuarioEntity);
        }

        [HttpPost("RedefinirSenha")]
        public async Task<ActionResult<ResponseEntity<UsuarioDto>>> RedefinirSenha(int usuarioId, string newSenha)
        {
            var usuarioEntity = await _usuarioInterface.RedefinirSenha(usuarioId, newSenha);

            return Ok(usuarioEntity);
        }

        [HttpPost("DeleteUsuario")]
        public async Task<ActionResult<ResponseEntity<UsuarioDto>>> DeleteUsuario(int usuarioId)
        {
            var usuarioEntity = await _usuarioInterface.DeleteUsuario(usuarioId);

            return Ok(usuarioEntity);
        }
    }
}
