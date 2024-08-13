namespace Gian.Basic.Helper;

public static class RandomHelper
{
    public static string GenerateDarkColor()
    {
        Random random = new();

        int r = random.Next(0, 128);
        int g = random.Next(0, 128);
        int b = random.Next(0, 128);

        return String.Format("#{0:X2}{1:X2}{2:X2}", r, g, b);
    }
}
