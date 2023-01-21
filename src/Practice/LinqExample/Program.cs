
List<int> myCollections = new List<int> {20,85,46,4,3,27,46,58,14,76};
var myNumbers = myCollections.Where(x => x > 20).OrderByDescending(y => y).ToList();
foreach (var value in myNumbers)
{
    Console.Write(value + " ");
}

//Find The Smallest value of this list
int smallValue = myCollections.Min();
Console.WriteLine("20,85,46,4,27,46,58,14,76");
Console.WriteLine($"The smallest value in this list is {smallValue}.");

//Find The Smallest Even value of this list
int smallestEvenVlaue = myCollections.Where(x => x % 2 == 0).Min();
Console.WriteLine($"The Smallest Even value in this list is {smallestEvenVlaue}.");
/*
 Note:We can also find Max(), Sum(), Avarage() as like same system
 */

Console.WriteLine();
List<string> namesCollections = new List<string> {"Shamim", "Anika", "Saba", "Fatema", "Moina","any"};
//var values = from i in namesCollections where i.Contains('m') select i;
var values = namesCollections.Where(x => x.StartsWith('S')).ToList();
foreach (var value in values)
{
    Console.Write(value + " ");
}
Console.WriteLine();

//find the smallest name in this list
 var samllestName = namesCollections.OrderBy(x => x.Count()).First();
Console.WriteLine("'Shamim', 'Anika', 'Saba', 'Fatema', 'Moina', 'any'");
Console.WriteLine($"The smallest Name in this list is: {samllestName}.");

Console.WriteLine("**************************Aggregate Function***********************");

//print all name separeted by coma as string
var printAllName = namesCollections.Aggregate((a,b) => a + "," + b);
Console.WriteLine(printAllName);
