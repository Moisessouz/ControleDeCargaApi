using System.Text.Json.Serialization;

namespace ControleDeCarga.Models
{
    public class StatusCarregamentoEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public ICollection<PlanejamentoCargaEntity> Cargas { get; set; }
    }
}
