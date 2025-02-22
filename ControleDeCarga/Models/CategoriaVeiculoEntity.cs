using System.Text.Json.Serialization;

namespace ControleDeCarga.Models
{
    public class CategoriaVeiculoEntity
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public int Capacidade { get; set; }
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public ICollection<VeiculoEntity> Veiculos { get; set; }
    }
}
