
List<int> myCollections = new List<int> {20,85,46,4,27,46,58,14,76};
var values  = from i in myCollections where i > 20 select i;
foreach (var value in values)
{
    Console.Write(value + " ");
}
