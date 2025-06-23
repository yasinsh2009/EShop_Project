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

    public static string CreateRandomPassword(int size = 0)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomString(4, true));
        builder.Append(RandomNumber(1000, 9999));
        builder.Append(RandomString(2, false));

        return builder.ToString();
    }

    public static string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }

    public static int RandomNumber(int min, int max)
    {
        // Generate a random number  
        Random random = new Random();
        // Any random integer   
        int num = random.Next(min, max);

        return num;
    }
}