namespace MyCipher;

internal static class ArrayExtenions
{
    public static T[,] Transpose<T>(this T[,] array)
    {
        int rows = array.GetLength(0);
        int columns = array.GetLength(1);

        T[,] result = new T[columns, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[j, i] = array[i, j];
            }
        }

        return result;
    }
}
