using System.Text.Json.Serialization;

namespace ControleDeCarga.Models
{
    public class MotoristaEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cnh { get; set; }
        public TransportadoraEntity Transportadora { get; set; }
        public bool Ativo {  get; set; }
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public ICollection<PlanejamentoCargaEntity> Cargas { get; set; }
    }
}
