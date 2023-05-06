// See https://aka.ms/new-console-template for more information
using JsonData;
using XMLFormetter;

//Course course = new Course();
Product product = new Product();

string XML = XmlFormatter.Convert(product);
Console.WriteLine(XML);

