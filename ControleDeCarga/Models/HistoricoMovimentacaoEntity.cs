namespace ControleDeCarga.Models
{
    public class HistoricoMovimentacaoEntity
    {
        public int Id { get; set; }
        public PlanejamentoCargaEntity Carga { get; set; }
        public int StatusAnterior { get; set; }
        public int StatusAtual { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
