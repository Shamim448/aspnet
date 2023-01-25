// See https://aka.ms/new-console-template for more information
using GenericsMethod;

GMethod generics = new GMethod();
{
    bool result = generics.commpire<float>(4.5f, 4.5f);
    Console.WriteLine(result);
}