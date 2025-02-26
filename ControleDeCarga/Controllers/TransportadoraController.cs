using Azure;
using ControleDeCarga.DTO.Transportadora;
using ControleDeCarga.Models;
using ControleDeCarga.Services.Transportadora;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCarga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportadoraController : ControllerBase
    {
        private readonly ITransportadoraService _transportadoraService;

        public TransportadoraController(ITransportadoraService transportadoraInterface) 
        {
            _transportadoraService = transportadoraInterface;
        }

        [HttpPost("CreateTransportadora")]
        public async Task<ActionResult<ResponseEntity<TransportadoraDto>>> CreateTransportadora(string nome, string cnpj)
        {
            var transportadoraEntity = await _transportadoraService.CreateTransportadora(nome, cnpj);

            return Ok(transportadoraEntity);
        }

        [HttpGet("GetAllTranportadoras")]
        public async Task<ActionResult<ResponseEntity<List<TransportadoraDto>>>> GetAllTransportadoras()
        {
            var transportadoras = await _transportadoraService.GetAllTransportadoras();

            return Ok(transportadoras);
        }
    }
}
