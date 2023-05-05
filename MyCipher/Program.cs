Console.WriteLine("Podaj text do zaszyfrowania: ");
string text = Console.ReadLine();
var encrypted = MyCipher.MyCipher.Encrypt(text);
Console.WriteLine(encrypted);

var decrepted = MyCipher.MyCipher.Decrypt(encrypted.ToString());
Console.WriteLine(decrepted);

using StreamWriter writetext = new ("encrypted_text.txt");
writetext.WriteLine(encrypted);