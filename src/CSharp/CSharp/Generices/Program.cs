// See https://aka.ms/new-console-template for more information
using Generices;

Employee<string> e = new Employee<string>();
e.Name = "Shamim";
e.Type = "Active";
Console.WriteLine($"Name: {e.Name} \nEmployee Type: {e.Type}");
Console.WriteLine();
e.print(new Employee<int>() { Name= "S", Type= 1});
