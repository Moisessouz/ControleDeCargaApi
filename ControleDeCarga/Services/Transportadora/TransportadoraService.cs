using ControleDeCarga.Data;
using ControleDeCarga.DTO.Transportadora;
using ControleDeCarga.DTO.Usuario;
using ControleDeCarga.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCarga.Services.Transportadora
{
    public class TransportadoraService : ITransportadoraService
    {
        private readonly AppDbContext _context;

        public TransportadoraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseEntity<TransportadoraDto>> CreateTransportadora(string nome, string cnpj)
        {
            var resposta = new ResponseEntity<TransportadoraDto>
            {
                Status = false
            };

            try
            {
                string formCnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

                if (formCnpj.Length != 14)
                {
                    resposta.Mensagem = "CNPJ inválido: Deve conter exatamente 14 dígitos!";
                    return resposta;
                }

                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await con.OpenAsync();

                    string query = @" INSERT INTO Transportadoras (Nome, Cnpj, Ativo, DataCriacao)
                              VALUES (@Nome, @Cnpj, 1, GETDATE());
                              SELECT SCOPE_IDENTITY();";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Nome", nome);
                        com.Parameters.AddWithValue("@Cnpj", formCnpj);

                        var result = await com.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int transportadoraId))
                        {
                            resposta.Dados = new TransportadoraDto
                            {
                                Id = transportadoraId,
                                Nome = nome,
                                Cnpj = formCnpj
                            };

                            resposta.Mensagem = "Transportadora criada com sucesso!";
                            resposta.Status = true;
                        }
                        else
                        {
                            resposta.Mensagem = "Erro ao cadastrar transportadora: ID inválido.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Erro ao cadastrar transportadora: " + ex.Message;
            }

            return resposta;
        }


        public async Task<ResponseEntity<List<TransportadoraDto>>> GetAllTransportadoras()
        {
            var resposta = new ResponseEntity<List<TransportadoraDto>>
            {
                Dados = new List<TransportadoraDto>(),
                Status = false
            };

            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await con.OpenAsync();

                    string query = @"SELECT Id,
                                            Nome,
                                            Cnpj,
                                            Ativo,
                                            DataCriacao
                                     FROM Transportadoras (NOLOCK)";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = await com.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var transportadora = new TransportadoraDto
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"].ToString(),
                                    Cnpj = reader["Cnpj"].ToString(),
                                    Ativo = Convert.ToBoolean(reader["Ativo"]),
                                    DataCriacao = Convert.ToDateTime(reader["DataCriacao"])
                                };

                                resposta.Dados.Add(transportadora);
                            };
                        }
                    }
                }

                if (resposta.Dados.Count > 0)
                {
                    resposta.Mensagem = "Transportadoras localizadas com sucesso!";
                    resposta.Status = true;
                }
                else
                {
                    resposta.Mensagem = "Nenhuma transportadora encontrada.";
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Erro ao buscar transportadoras: " + ex.Message;
                resposta.Status = false;
            }

            return resposta;
        }
    }
}
