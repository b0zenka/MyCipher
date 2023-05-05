using System.Text;

namespace MyCipher;

internal static class MyCipher
{
    #region private variables
    private const int LETTERS_IN_ROW = 40;
    private static readonly Random random = new Random();
    private static readonly Dictionary<char, char[]> KEY = new Dictionary<char, char[]>()
    {
        {'e', new char [] {'t', '*', 'z'}},
        {'t', new char[] { 'i', 's', 'u'}},
        {'o', new char[] { 'r', 'l', '#'}},
        {'a', new char [] { 'm', '='}},
        {'i', new char [] { 'h', '&'}},
        {'s', new char [] { '?', 'w'}},
        {'u', new char [] { '%', 'k'}},
        {'r', new char [] { 'c', 'p'}},
        {'l', new char [] { 'a', 'n'}},
        {'n', new char [] { 'd', '$'}},
        {'h', new char [] { 'j', 'q'}},
        {'y', new char [] { '@'}},
        {'m', new char [] { 'f'}},
        {'g', new char [] { 'v'}},
        {'d', new char [] { 'x'}},
        {'f', new char [] { 'y'}},
        {'b', new char [] { '!'}},
        {'w', new char [] { 'b'}},
        {'c', new char [] { 'o'}},
        {'p', new char [] { '+'}},
        {'v', new char [] { 'g'}},
        {'k', new char [] { '/'}},
        {'j', new char [] { '~'}},
        {'x', new char [] { '^'}},
        {'z', new char [] { '<'}},
        {'q', new char [] { '.'}},
    };
    #endregion

    public static StringBuilder Encrypt(string text)
    {
        if (String.IsNullOrEmpty(text))
            return null;

        text = text.RemoveSpecialCharacters().ToLower();
        char[,] replacedCharacterTab = GetReplacedCharacterTable(text);
        var transposed = replacedCharacterTab.Transpose();

        return BuildResult(transposed);
    }

    public static StringBuilder Decrypt(string text)
    {
        if (String.IsNullOrEmpty(text))
            return null;

        char[,] characterTable = GetCharacterTable(text);
        characterTable = characterTable.Transpose();
        return BuildResult(characterTable, false);
    }

    private static char[,] GetCharacterTable(string text)
    {
        var textWithoutWhiteSpaces = text.Replace(" ", "").Replace("\n", "");
        var cols = (int)Math.Ceiling(textWithoutWhiteSpaces.Length / (decimal)LETTERS_IN_ROW);
        var result = new char[LETTERS_IN_ROW, cols];
        int count = 0;

        for (int i = 0;  i < LETTERS_IN_ROW; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (count >= textWithoutWhiteSpaces.Length)
                {
                    result[i, j] = '\0';
                    continue;
                }

                var letter = textWithoutWhiteSpaces[count++];
                var changed = KEY.FirstOrDefault(x => x.Value.Contains(letter)).Key;
                result[i, j] = changed;
            }
        }

        return result;
    }

    #region private methods

    private static StringBuilder BuildResult(char[,] result, bool addspace = true)
    {
        StringBuilder stringBuilder = new StringBuilder();
        int numRows = result.GetLength(0);
        int numCols = result.GetLength(1);

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                stringBuilder.Append(result[i, j]);

                if ((j + 1) % 5 == 0 && addspace)
                    stringBuilder.Append(" ");
            }
            if (addspace)
                stringBuilder.Append("\n");
        }

        return stringBuilder;
    }

    private static char[,] GetReplacedCharacterTable(string text)
    {
        var rows = (int) Math.Ceiling(text.Length / (decimal)LETTERS_IN_ROW);
        var result = new char[rows, LETTERS_IN_ROW];

        for (int i = 0, count = 0; i <= rows; i++)
        {
            for (var j = 0; j < LETTERS_IN_ROW && count < text.Length; j++)
            {
                var letter = text[count++];
                var replacements = KEY[letter];
                var choice = random.Next(replacements.Length);
                result[i, j] = replacements[choice];
            }
        }

        return result;
    }

    #endregion
}
