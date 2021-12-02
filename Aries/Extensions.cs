namespace Aries;

public static class Extensions
{
    public static int? ToIntNullable(this string s)
    {
        if (string.IsNullOrWhiteSpace(s)) { return null; }
        return Convert.ToInt32(s);
    }

    public static int ToInt(this string s)
    {
        return Convert.ToInt32(s);
    }
}
