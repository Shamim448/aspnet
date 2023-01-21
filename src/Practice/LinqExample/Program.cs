
//List<int> myCollections = new List<int> {20,85,46,4,27,46,58,14,76};
List<string> myCollections = new List<string> {"Shamim", "Anika", "Saba", "Fatema", "Moina"};
var values = from i in myCollections where i.Contains('m') select i;
foreach (var value in values)
{
    Console.Write(value + " ");
}

