using System.Text;

namespace MyCipher;

internal static class StringExtensions
{
    public static string RemoveSpecialCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}
