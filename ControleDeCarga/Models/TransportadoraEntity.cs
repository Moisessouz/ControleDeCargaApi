using System.Text.Json.Serialization;

namespace ControleDeCarga.Models
{
    public class TransportadoraEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public ICollection<VeiculoEntity> Veiculos { get; set; }
        [JsonIgnore]
        public ICollection<MotoristaEntity> Motoristas { get; set; }
    }
}
