using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Linq;

namespace GeneralMerchandise.Data.Password
{
    internal class HashedPassword : IHashedPassword
    {
        private static readonly int SALT_SIZE = 16;

        /// <inheritdoc />
        SecuredPassword IHashedPassword.HashPassword(string password)
        {
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] salt = CreateSalt(rng, SALT_SIZE);
            string saltString = ConvertToString(salt);
            return new SecuredPassword(saltString, ConvertToString(GetHash(saltString, password)));
        }
        
        public bool VerifyPassword(string input, SecuredPassword password)
        {
            return ConvertToString(
                GetHash(password.Salt.GetStringFinalize(), input)
                ).Equals(password.HashedPassword.GetStringFinalize());
        }

        public byte[] GetHash(string salt, string password)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            HashAlgorithm hash = new SHA1CryptoServiceProvider();
            return hash.ComputeHash(PreppendBytes(saltBytes, passwordBytes));
        }

        private static byte[] CreateSalt(RandomNumberGenerator randomNumberGenerator, int size)
        {
            byte[] bytes = new Byte[size];
            randomNumberGenerator.GetBytes(bytes);
            return bytes;
        }

        private byte[] PreppendBytes(byte[] bytes, byte[] toPreppend)
        {
            return toPreppend.Concat(bytes).ToArray();
        }

        private string ConvertToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes) sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }
        
    }
}
