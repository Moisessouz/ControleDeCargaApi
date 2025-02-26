using ControleDeCarga.DTO.Transportadora;
using ControleDeCarga.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCarga.Services.Transportadora
{
    public interface ITransportadoraService
    {
        Task<ResponseEntity<TransportadoraDto>> CreateTransportadora(string nome, string cnpj);
        Task<ResponseEntity<List<TransportadoraDto>>> GetAllTransportadoras();
    }
}
