using System.Text;

namespace Gian.Basic.Helper;

public static class ByteHelper
{
    public static byte[] ScrambleBytes(this byte[] bytes)
    {
        byte[] scrambledBytes = new byte[bytes.Length];

        for (int i = 0; i < bytes.Length; i++)
        {
            scrambledBytes[i] = (byte)(bytes[i] ^ (i % 256));
        }

        return scrambledBytes;
    }

    public static string ToStringRepresentation(this byte[] byteArray)
    {
        return Encoding.UTF8.GetString(byteArray);
    }
}
