using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Dynu.API.Model.IPUpdater
{
    public interface IAuth
    {
        void Apply(WebClient client, ref string url);
    }
    public abstract class HashAuth : IAuth
    {
        private readonly string _Username;
        private readonly string _PasswordHash;
        private readonly string _HashAlgorithm;
        public HashAuth(string username, byte[] password, string hashAlgorithm)
        {
            if (username == null)
                throw new ArgumentNullException("username cannot be null.");
            if (password == null)
                throw new ArgumentNullException("password cannot be null.");
            if (hashAlgorithm == null)
                throw new ArgumentNullException("hash algorithm cannot be null.");

            _Username = username;
            _PasswordHash = HashPassword(password);
            _HashAlgorithm = hashAlgorithm;
        }
        private string HashPassword(byte[] password)
        {
            using (var hash = HashAlgorithm.Create(_HashAlgorithm))
            {
                var bytes = hash.ComputeHash(password);

                for (int i = 0; i < password.Length; i++)
                    password[i] = 0;

                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }
        public void Apply(WebClient client, ref string url)
        {
            url += $"&password={_HashAlgorithm}({_PasswordHash})";
        }
    }
    public class MD5Auth : HashAuth
    {
        public MD5Auth(string username, byte[] password) : base(username, password, "MD5")
        {
        }
    }
    public class SHA256Auth : HashAuth
    {
        public SHA256Auth(string username, byte[] password) : base(username, password, "SHA256")
        {
        }
    }
    public class BasicHeaderAuth : IAuth
    {
        private readonly string _Header;
        public BasicHeaderAuth(string username, string password)
        {
            _Header = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
        }
        public void Apply(WebClient client, ref string url)
        {
            client.Headers.Add("Authorization", "Basic " + _Header);
        }
    }
}
