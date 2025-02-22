using System.Text.Json.Serialization;

namespace ControleDeCarga.Models
{
    public class UsuarioHierarquiaEntity
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public ICollection<UsuarioEntity> Usuarios { get; set; }
    }
}
