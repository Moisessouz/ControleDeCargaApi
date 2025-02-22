using ControleDeCarga.DTO.Usuario;
using ControleDeCarga.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCarga.Services.Usuario
{
    public interface IUsuarioService
    {
        Task<ResponseEntity<List<UsuarioDto>>> GetAllUsuarios();
        Task<ResponseEntity<UsuarioDto>> GetUsuarioById(int usuarioId);
        Task<ResponseEntity<CreateUsuarioDto>> CreateNewUsuario(CreateUsuarioDto newUsuario);
        Task<ResponseEntity<UsuarioDto>> Login(string email, string senha);
        Task<ResponseEntity<UsuarioDto>> RedefinirSenha(int usuarioId, string newSenha);
        Task<ActionResult<ResponseEntity<UsuarioDto>>> DeleteUsuario(int usuarioId);
    }
}
