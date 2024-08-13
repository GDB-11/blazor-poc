namespace Gian.Basic.Helper;

public  static class SvgHelper
{
    public static string GenerateSvg(char letter)
    {
        Random random = new();
        string color = GenerateDarkColor(random);

        string svg = $@"
        <svg xmlns='http://www.w3.org/2000/svg'>
            <circle cx='50' cy='50' r='40' fill='{color}' />
            <text x='50%' y='50%' font-family='Arial' font-size='40' fill='white' text-anchor='middle' alignment-baseline='middle'>{letter}</text>
        </svg>";

        return svg;
    }

    private static string GenerateDarkColor(Random random)
    {
        int r = random.Next(0, 128);
        int g = random.Next(0, 128);
        int b = random.Next(0, 128);
        return String.Format("#{0:X2}{1:X2}{2:X2}", r, g, b);
    }
}
