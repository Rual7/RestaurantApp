using System.Security.Cryptography;
using System.Text;

namespace BusinessLogicLayer.Helpers;

public static class PasswordHelper
{
    public static string HashPassword(
        string password)
    {
        byte[] bytes =
            SHA256.HashData(
                Encoding.UTF8.GetBytes(password));

        StringBuilder builder =
            new();

        foreach (byte value in bytes)
        {
            builder.Append(
                value.ToString("x2"));
        }

        return builder.ToString();
    }
}