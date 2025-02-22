using System.Text.Json.Serialization;

namespace ControleDeCarga.Models
{
    public class PlanejamentoCargaEntity
    {
        public string Id { get; set; }
        public VeiculoEntity Veiculo { get; set; }
        public MotoristaEntity Motorista { get; set; }
        public int Peso { get; set; }
        public StatusCarregamentoEntity Status { get; set;}
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        [JsonIgnore]
        public ICollection<HistoricoMovimentacaoEntity> Movimentacao { get; set; }
    }
}