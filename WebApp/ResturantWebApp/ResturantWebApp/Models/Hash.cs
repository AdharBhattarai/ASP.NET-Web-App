using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;


namespace ResturantWebApp.Models
{
    public static class Hash
    {
        //https://monkelite.com/how-to-hash-password-in-asp-net-core/

        // Reference Taken From Professor Holmes for password hashing and salt.

        public static string HashIt(string pass, string salt)
        {
            byte[] hasher = KeyDerivation.Pbkdf2(pass, Encoding.UTF8.GetBytes(salt),
            KeyDerivationPrf.HMACSHA256, 1000, 256 / 8);
            return Convert.ToBase64String(hasher);
        }
        // Reference Taken From Professor Holmes for generating salt.

        // Generates random Salt
        public static string GenerateSalt()
        {
            byte[] rndBytes = new byte[128 / 8];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(rndBytes);
            return Convert.ToBase64String(rndBytes);
        }
    }
}
