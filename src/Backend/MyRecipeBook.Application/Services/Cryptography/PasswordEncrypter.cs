using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.Services.Cryptography
{
    public class PasswordEncrypter
    {
        private readonly string _additionalKey;

        // Construtor que recebe a chave adicional para a criptografia vindo da injeção de dependência
        public PasswordEncrypter(string additionalKey) => _additionalKey = additionalKey;

        /* Função que faz a códificação da senha passada */
        public string Encrypt(string password)
        {
            var newPassword = $"{password}{_additionalKey}"; // Concatena a senha com a chave adicional
            var bytes = Encoding.UTF8.GetBytes(newPassword);   // Converte a senha em um array de bytes
            var hashBytes = SHA512.HashData(bytes);         // Criptografa a senha usando o algoritmo SHA-512

            return StringBytes(hashBytes);
        }

        // Converte um array de bytes em uma string hexadecimal
        private static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();    // Usado para construir a string de forma eficiente

            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2")); // Converte cada byte em uma representação hexadecimal de dois dígitos
            }

            return sb.ToString();
        }
    }
}
