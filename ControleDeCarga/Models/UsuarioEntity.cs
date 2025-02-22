
namespace ControleDeCarga.Models
{
    public class UsuarioEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] Senha { get; set; }
        public string Email { get; set; }
        public string TipoUsuarioId { get; set; }
        public UsuarioHierarquiaEntity TipoUsuario { get; set; }
        public bool Ativo { get; set;}
        public bool Bloqueado { get; set;}
        public int TentativasAcesso { get; set;}
        public bool PrimeiroAcesso { get; set;}
        public DateTime DataCriacao { get; set;}
    }
}
