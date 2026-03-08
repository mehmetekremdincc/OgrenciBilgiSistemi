using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using OgrenciBilgiSistemiProject.Services.Abstract;

namespace OgrenciBilgiSistemiProject.Services.Concrete
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int Iterations = 100000;
        private const int KeySize = 32;

        public string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(16);

            var hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize);

            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            try
            {
                var hashToVerify = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: Iterations,
                    numBytesRequested: KeySize);

                return CryptographicOperations.FixedTimeEquals(
                    hashToVerify,
                    Convert.FromBase64String(hash));
            }
            catch (FormatException)
            {
                return password == hash;
            }
        }
    }
}