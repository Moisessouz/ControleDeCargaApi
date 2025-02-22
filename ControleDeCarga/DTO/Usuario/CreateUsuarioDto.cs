using ControleDeCarga.Models;
using System.Text.Json.Serialization;

namespace ControleDeCarga.DTO.Usuario
{
    public class CreateUsuarioDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Nome { get; set; }
        [JsonIgnore]
        public string? Senha { get; set; }
        public string? Email { get; set; }
        public string? TipoUsuario { get; set; }
        [JsonIgnore]
        public bool Ativo { get; set; }
        [JsonIgnore]
        public bool Bloqueado { get; set; }
        [JsonIgnore]
        public int TentativasAcesso { get; set; }
        [JsonIgnore]
        public bool PrimeiroAcesso { get; set; }
        [JsonIgnore]
        public DateTime DataCriacao { get; set; }
    }
}
