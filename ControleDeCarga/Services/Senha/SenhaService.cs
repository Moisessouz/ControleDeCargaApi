using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace ControleDeCarga.Services.Senha
{
    public class SenhaService
    {
        /// <summary>
        /// Gera uma senha aleatória com caracteres maiúsculos, minúsculos, números e caracteres especiais.
        /// </summary>
        /// <param name="tamanho">O tamanho desejado para a senha gerada.</param>
        /// <returns>Uma string contendo a senha aleatória gerada.</returns>
        public string GerarSenhaAleatoria(int tamanho)
        {
            const string letrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
            const string numeros = "0123456789";
            const string especiais = "@#$%&*!?";
            const string todosCaracteres = letrasMaiusculas + letrasMinusculas + numeros + especiais;

            StringBuilder senha = new StringBuilder();

            //Gera números aleatórios
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                // Garante que tenha pelo menos um caracter de cada tipo
                senha.Append(letrasMaiusculas[ObterIndiceAleatorio(rng, letrasMaiusculas.Length)]);
                senha.Append(letrasMinusculas[ObterIndiceAleatorio(rng, letrasMinusculas.Length)]);
                senha.Append(numeros[ObterIndiceAleatorio(rng, numeros.Length)]);
                senha.Append(especiais[ObterIndiceAleatorio(rng, especiais.Length)]);

                // Preencher o restante aleatoriamente
                for (int i = 4; i < tamanho; i++)
                {
                    senha.Append(todosCaracteres[ObterIndiceAleatorio(rng, todosCaracteres.Length)]);
                }
            }

            // Embaralha os caracteres para evitar previsibilidade
            return new string(senha.ToString().ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

        /// <summary>
        /// Obtém um índice aleatório para selecionar um caractere de um conjunto de caracteres.
        /// </summary>
        /// <param name="rng">Objeto para geração de números aleatórios seguros.</param>
        /// <param name="maxLength">O comprimento máximo do conjunto de caracteres.</param>
        /// <returns>Um índice aleatório dentro do intervalo de 0 a maxLength.</returns>
        private static int ObterIndiceAleatorio(RandomNumberGenerator rng, int maxLength)
        {
            byte[] data = new byte[1];
            rng.GetBytes(data);
            return data[0] % maxLength;
        }

                /// <summary>
        /// Gera um hash seguro da senha usando o algoritmo BCrypt.
        /// </summary>
        /// <param name="senha">A senha que será convertida para hash.</param>
        /// <returns>O hash gerado da senha.</returns>
        public string HashSenha(string senha)
        {
            // Aqui você pode personalizar o número de rounds, se necessário
            return BCrypt.Net.BCrypt.HashPassword(senha, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        /// <summary>
        /// Verifica se a senha fornecida corresponde ao hash armazenado.
        /// </summary>
        /// <param name="senha">A senha fornecida pelo usuário.</param>
        /// <param name="senhaHash">O hash da senha armazenada no banco de dados.</param>
        /// <returns>Retorna true se as senhas corresponderem, caso contrário, retorna false.</returns>
        public bool VerificaSenha(string senha, string senhaHash)
        {
            // Verifica se a senha fornecida corresponde ao hash armazenado
            return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
        }
    }
}
