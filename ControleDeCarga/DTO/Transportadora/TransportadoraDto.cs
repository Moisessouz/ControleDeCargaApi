namespace ControleDeCarga.DTO.Transportadora
{
    public class TransportadoraDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
