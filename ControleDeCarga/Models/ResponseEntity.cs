namespace ControleDeCarga.Models
{
    public class ResponseEntity<T>
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set;} = string.Empty;
        public bool Status { get; set; } = true;
    }
}
