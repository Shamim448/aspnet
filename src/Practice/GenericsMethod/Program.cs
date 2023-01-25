// See https://aka.ms/new-console-template for more information
using GenericsMethod;

GMethod generics = new GMethod();
{
    bool result = generics.commpire<float>(4.5f, 4.5f);
    Console.WriteLine(result);
}

//Generics Class
GenericClass<int> gClass = new GenericClass<int>();
Console.WriteLine( gClass.Add(10, 10));
Console.WriteLine( gClass.Sub(10, 10));
Console.WriteLine( gClass.Mul(10, 10));
Console.WriteLine( gClass.Div(10, 10 ));