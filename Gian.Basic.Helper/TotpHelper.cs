using OtpNet;
using System.Drawing;

namespace Gian.Basic.Helper;

public static class TotpHelper
{
    public static string GenerateTOTP(string secret)
    {
        byte[] otpKey = Base32Encoding.ToBytes(secret);

        Totp totp = new(otpKey);

        return totp.ComputeTotp();
    }

    public static bool VerifyTOTP(string secret, string code)
    {
        byte[] otpKey = Base32Encoding.ToBytes(secret);

        Totp totp = new(otpKey);

        return totp.VerifyTotp(code, out long timeStepMatched, new VerificationWindow(2, 2));
    }
}
