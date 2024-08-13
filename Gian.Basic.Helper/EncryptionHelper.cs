using System.Security.Cryptography;
using System.Text;

namespace Gian.Basic.Helper;

public static class EncryptionHelper
{
    public static string EncryptWithKey(string key, string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.GenerateIV();

        using MemoryStream memoryStream = new();

        memoryStream.Write(aes.IV, 0, aes.IV.Length);

        using (CryptoStream cryptoStream = new(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            using StreamWriter streamWriter = new(cryptoStream);

            streamWriter.Write(plainText);
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string DecryptWithKey(string key, string cipherText)
    {
        byte[] fullCipher = Convert.FromBase64String(cipherText);

        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        byte[] iv = new byte[aes.IV.Length];
        Array.Copy(fullCipher, 0, iv, 0, iv.Length);
        aes.IV = iv;

        using MemoryStream memoryStream = new(fullCipher, iv.Length, fullCipher.Length - iv.Length);

        using CryptoStream cryptoStream = new(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);

        using StreamReader streamReader = new(cryptoStream);

        return streamReader.ReadToEnd();
    }

    public static string EncryptWithMAC(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = NetworkHelper.GetMacAddress().HashString().ScrambleBytes();
        aes.GenerateIV();
        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using MemoryStream ms = new();

        using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
        {
            using StreamWriter sw = new(cs);
            sw.Write(plainText);
        }

        byte[] iv = aes.IV;
        byte[] encryptedContent = ms.ToArray();
        byte[] result = new byte[iv.Length + encryptedContent.Length];
        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(encryptedContent, 0, result, iv.Length, encryptedContent.Length);

        return Convert.ToBase64String(result);
    }

    public static string DecryptWithMAC(string cipherText)
    {
        byte[] fullCipher = Convert.FromBase64String(cipherText);

        using Aes aes = Aes.Create();
        byte[] iv = new byte[aes.BlockSize / 8];
        byte[] cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        aes.Key = NetworkHelper.GetMacAddress().HashString().ScrambleBytes();
        aes.IV = iv;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using MemoryStream ms = new(cipher);

        using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);

        using StreamReader sr = new(cs);

        return sr.ReadToEnd();
    }
}