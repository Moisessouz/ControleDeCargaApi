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

        [HttpPost("CreateUsuario")]
        public async Task<ActionResult<ResponseEntity<UsuarioDto>>> CreateUsuario(string nome, string email, string tipoUsuario)
        {
            var usuarioEntity = await _usuarioInterface.CreateUsuario(nome, email,tipoUsuario);

            return Ok(usuarioEntity);
        }

        [HttpPost("ResetSenha")]
        public async Task<ActionResult<ResponseEntity<UsuarioDto>>> ResetSenha(int usuarioId, string newSenha)
        {
            var usuarioEntity = await _usuarioInterface.ResetSenha(usuarioId, newSenha);

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
