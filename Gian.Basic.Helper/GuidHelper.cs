namespace Gian.Basic.Helper;

public static class GuidHelper
{
    public static string GenerateGuid()
    {
        return Ulid.NewUlid().ToGuid().ToString("N");
    }
}
