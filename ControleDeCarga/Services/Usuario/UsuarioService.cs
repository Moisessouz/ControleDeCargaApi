using ControleDeCarga.Data;
using ControleDeCarga.DTO.Usuario;
using ControleDeCarga.Models;
using ControleDeCarga.Services.Email;
using ControleDeCarga.Services.Senha;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ControleDeCarga.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;
        private readonly SenhaService _senhaService;
        private readonly EmailService _emailService;

        public UsuarioService(AppDbContext context, SenhaService senhaService, EmailService emailService)
        {
            _context = context;
            _senhaService = senhaService;
            _emailService = emailService;
        }

        public async Task<ResponseEntity<UsuarioDto>> Login(string email, string senha)
        {
            var resposta = new ResponseEntity<UsuarioDto>
            {
                Dados = new UsuarioDto(),
                Status = false
            };

            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await con.OpenAsync();

                    string query = @"SELECT Id, 
                                            Nome, 
                                            Email, 
                                            Senha, 
                                            Ativo, 
                                            Bloqueado
                                     FROM Usuario 
                                     WHERE Email = @Email";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = await com.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string senhaHash = reader["Senha"].ToString();
                                bool senhaValida = _senhaService.VerificaSenha(senha, senhaHash);

                                if (senhaValida)
                                {
                                    // Autenticação bem-sucedida
                                    resposta.Dados = new UsuarioDto
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Nome = reader["Nome"].ToString(),
                                        Email = reader["Email"].ToString(),
                                    };

                                    resposta.Mensagem = "Login realizado com sucesso!";
                                    resposta.Status = true;
                                }
                                else
                                {
                                    resposta.Mensagem = "Senha incorreta.";
                                }
                            }
                            else
                            {
                                resposta.Mensagem = "Usuário não encontrado.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Erro ao realizar login: " + ex.Message;
                resposta.Status = false;
            }

            return resposta;
        }

        public async Task<ResponseEntity<List<UsuarioDto>>> GetAllUsuarios()
        {
            var resposta = new ResponseEntity<List<UsuarioDto>>
            {
                Dados = new List<UsuarioDto>(),
                Status = false
            };

            try
            {

                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await con.OpenAsync();

                    string query = @"SELECT Id, 
                                            Nome, 
                                            Email, 
                                            Ativo, 
                                            Bloqueado, 
                                            TipoUsuarioId, 
                                            DataCriacao 
                                     FROM Usuario (NOLOCK)";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = await com.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var usuario = new UsuarioDto
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Ativo = Convert.ToBoolean(reader["Ativo"]),
                                    Bloqueado = Convert.ToBoolean(reader["Bloqueado"]),
                                    TipoUsuario = reader["TipoUsuarioId"].ToString(),
                                    DataCriacao = Convert.ToDateTime(reader["DataCriacao"])
                                };

                                resposta.Dados.Add(usuario);
                            }
                        }
                    }
                }

                if (resposta.Dados.Count > 0)
                {
                    resposta.Mensagem = "Usuários localizados com sucesso!";
                    resposta.Status = true;
                }
                else
                {
                    resposta.Mensagem = "Nenhum usuário encontrado.";
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Erro ao buscar usuários: " + ex.Message;
                resposta.Status = false;
            }

            return resposta;
        }

        public async Task<ResponseEntity<UsuarioDto>> GetUsuarioById(int usuarioId)
        {
            var resposta = new ResponseEntity<UsuarioDto>
            {
                Dados = new UsuarioDto(),
                Status = false
            };

            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await con.OpenAsync();

                    string query = @"SELECT Id, 
                                            Nome, 
                                            Email, 
                                            Ativo, 
                                            Bloqueado, 
                                            TipoUsuarioId, 
                                            DataCriacao 
                                     FROM Usuario (NOLOCK)
                                     WHERE Id = @Usuario";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Usuario", usuarioId);

                        using (SqlDataReader reader = await com.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                resposta.Dados = new UsuarioDto
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nome = reader["Nome"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Ativo = Convert.ToBoolean(reader["Ativo"]),
                                    Bloqueado = Convert.ToBoolean(reader["Bloqueado"]),
                                    TipoUsuario = reader["TipoUsuarioId"].ToString(),
                                    DataCriacao = Convert.ToDateTime(reader["DataCriacao"])
                                };

                                resposta.Mensagem = "Usuário localizado com sucesso!";
                                resposta.Status = true;
                            }
                            else
                            {
                                resposta.Mensagem = "Usuário não encontrado.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Erro ao buscar usuário: " + ex.Message;
                resposta.Status = false;
            }

            return resposta;
        }

        public async Task<ResponseEntity<UsuarioDto>> CreateUsuario(string nome, string email, string tipoUsuario)
        {
            var resposta = new ResponseEntity<UsuarioDto> { Status = false };

            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await con.OpenAsync();

                    string senhaTemporaria = _senhaService.GerarSenhaAleatoria(12);
                    string senhaHash = _senhaService.HashSenha(senhaTemporaria);

                    string query = @"INSERT INTO Usuario (Nome, Senha, Email, Ativo, Bloqueado, TentativasAcesso, TipoUsuarioId, DataCriacao, PrimeiroAcesso)
                                    VALUES (@Nome, @Senha, @Email, 1, 0, 0, @TipoUsuario, GETDATE(), 1);
                                    SELECT SCOPE_IDENTITY();";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Nome", nome);
                        com.Parameters.AddWithValue("@Senha", senhaHash);
                        com.Parameters.AddWithValue("@Email", email);
                        com.Parameters.AddWithValue("@TipoUsuario", tipoUsuario);

                        var result = await com.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int newUserId))
                        {
                            resposta.Dados = new UsuarioDto
                            {
                                Id = newUserId,
                                Nome = nome,
                                Email = email,
                            };

                            // Enviar um e-mail com a senha temporária
                            //string assunto = "Seja bem-vindo(a) ao Sistema";
                            //string corpo = $"Olá {newUsuario.Nome},\n\nSua conta foi criada com sucesso! " +
                            //      $"Sua senha temporária é: {senhaTemporaria}\n" +
                            //      "Por favor, faça o login e redefina sua senha no primeiro acesso."; ;
                            //await _emailService.SendEmailAsync(newUsuario.Email, assunto, corpo);

                            resposta.Mensagem = "Usuário criado com sucesso!";
                            resposta.Status = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Erro ao cadastrar usuário: " + ex.Message;
            }

            return resposta;
        }

        public async Task<ResponseEntity<UsuarioDto>> ResetSenha(int usuarioId, string newSenha)
        {
            var resposta = new ResponseEntity<UsuarioDto>
            {
                Status = false
            };

            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await con.OpenAsync();

                    string senhaHash = _senhaService.HashSenha(newSenha);

                    string query = @"UPDATE Usuario
                                    SET Senha = @Senha
                                    WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Id", usuarioId);
                        com.Parameters.AddWithValue("@Senha", senhaHash);

                        int usuario = await com.ExecuteNonQueryAsync();

                        if (usuario > 0)
                        {
                            resposta.Mensagem = "Senha atualizada com sucesso!";
                            resposta.Status = true;
                        }
                        else
                        {
                            resposta.Mensagem = "Usuário não encontrado.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Erro ao redefinir senha: " + ex.Message;
            }

            return resposta;
        }

        public async Task<ActionResult<ResponseEntity<UsuarioDto>>> DeleteUsuario(int usuarioId)
        {
            var resposta = new ResponseEntity<UsuarioDto>
            {
                Status = false
            };

            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    await con.OpenAsync();

                    string query = @"DELETE FROM Usuario WHERE Id = @Id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@Id", usuarioId);

                        int usuario = await com.ExecuteNonQueryAsync();

                        if (usuario > 0)
                        {
                            resposta.Mensagem = "Usuário deletado com sucesso!";
                            resposta.Status = true;
                        }
                        else
                        {
                            resposta.Mensagem = "Usuário não encontrado.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.Mensagem = "Erro ao deletar usuário: " + ex.Message;
            }

            return resposta;
        }

    }
}
