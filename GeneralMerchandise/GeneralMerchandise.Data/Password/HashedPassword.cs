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

        SecuredPassword IHashedPassword.HashPassword(string password)
        {
            HashAlgorithm hash = new SHA1CryptoServiceProvider();
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] salt = CreateSalt(rng, SALT_SIZE);
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            return new SecuredPassword(ConvertToString(salt), ConvertToString(hash.ComputeHash(PreppendBytes(salt, passwordBytes))));
        }


        public bool VerifyPassword(string input, SecuredPassword password)
        {
            throw new NotImplementedException();
        }

        //public bool VerifyPassword(string input, string storedPassword)
        //{
        //    return storedPassword.Equals(HashPassword(input));
        //}

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
