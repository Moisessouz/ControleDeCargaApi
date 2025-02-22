
using System.Text.Json.Serialization;

namespace ControleDeCarga.DTO.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        [JsonIgnore]
        public string? Senha { get; set; }
        public string? Email { get; set; }
        public string? TipoUsuario { get; set; }
        public bool Ativo { get; set; }
        public bool Bloqueado { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}