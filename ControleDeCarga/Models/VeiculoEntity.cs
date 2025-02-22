using System.Text.Json.Serialization;

namespace ControleDeCarga.Models
{
    public class VeiculoEntity
    {
        public int Id { get; set; } 
        public string Placa { get; set; }
        public CategoriaVeiculoEntity Categoria { get; set; }
        public TransportadoraEntity Transportadora { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public ICollection<PlanejamentoCargaEntity> Cargas { get; set; }
    }
}