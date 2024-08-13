using System.Reflection;

namespace Gian.Basic.Helper;

public static class PathHelper
{
    public static string ExecutingLocation()
    {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
    }
}
