using System.Security.Cryptography;
using System.Text;

namespace EShop.Application.Utilities;

public class Sha256Example
{
    public static void Main()
    {
        string input = "Hello, this is a test string!";
        string hashedOutput = ComputeSHA256Hash(input);

        Console.WriteLine("Original Input: " + input);
        Console.WriteLine("SHA256 Hash: " + hashedOutput);
    }

    // تابع برای محاسبه SHA256
    public static string ComputeSHA256Hash(string input)
    {
        // ایجاد یک شیء SHA256Managed
        using (SHA256 sha256 = SHA256.Create())
        {
            // تبدیل رشته ورودی به آرایه بایت
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            // محاسبه هش
            byte[] hashBytes = sha256.ComputeHash(bytes);

            // تبدیل آرایه بایت هش به رشته هگزادسیمال
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}