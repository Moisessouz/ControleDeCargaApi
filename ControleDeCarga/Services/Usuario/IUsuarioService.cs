using ControleDeCarga.DTO.Usuario;
using ControleDeCarga.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCarga.Services.Usuario
{
    public interface IUsuarioService
    {
        Task<ResponseEntity<List<UsuarioDto>>> GetAllUsuarios();
        Task<ResponseEntity<UsuarioDto>> GetUsuarioById(int usuarioId);
        Task<ResponseEntity<UsuarioDto>> CreateUsuario(string nome, string email, string tipoUsuario);
        Task<ResponseEntity<UsuarioDto>> Login(string email, string senha);
        Task<ResponseEntity<UsuarioDto>> ResetSenha(int usuarioId, string newSenha);
        Task<ActionResult<ResponseEntity<UsuarioDto>>> DeleteUsuario(int usuarioId);
    }
}
