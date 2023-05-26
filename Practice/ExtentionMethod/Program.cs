// See https://aka.ms/new-console-template for more information

using ExtentionMethod;

Console.OutputEncoding = System.Text.Encoding.Unicode;

string message = " Hello, How are you? ";
string result = message.AddSmaileFace();

Console.WriteLine(message.AddSmaileFace());
Console.WriteLine(result);

