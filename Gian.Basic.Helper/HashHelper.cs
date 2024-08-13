using System.Security.Cryptography;
using System.Text;

namespace Gian.Basic.Helper;

public static class HashHelper
{
    public static byte[] HashString(this string text)
    {
        return SHA256.HashData(Encoding.UTF8.GetBytes(text));
    }
}
