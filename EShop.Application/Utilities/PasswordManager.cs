using System.Security.Cryptography;
using System.Text;

namespace EShop.Application.Utilities;

public static class PasswordManager
{
    public static string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        return Convert.ToHexString(hashBytes).ToLower();
    }

    public static string GenerateSalt(int length)
    {
        var saltBytes = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);
        return Convert.ToHexString(saltBytes).ToLower();
    }

    public static bool VerifyPassword(string password, string salt, string hashedPassword)
    {
        string computedHash = HashPassword(password, salt);
        return hashedPassword == computedHash;
    }
}