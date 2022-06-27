using Datalogi_Inlamning;

BinarySearchTree<int> myTree = new();
const int SAMPLE_SIZE = 100;
var sample = new int[SAMPLE_SIZE];
var rng = new Random();
for (int i = 0; i < SAMPLE_SIZE; i++)
{
    sample[i] = rng.Next(1,101);
    //sample[i] = i;
}
foreach (var num in sample)
{
    myTree.Insert(num);
}
Console.WriteLine("balance: " + myTree.GetBalance());
Console.WriteLine("count: " + myTree.Count());
//Console.WriteLine("Does 56 exist in tree: " + myTree.Exists(56));
//Console.WriteLine("Does -56 exist in tree: " + myTree.Exists(-56));
//Console.WriteLine("Does 566 exist in tree: " + myTree.Exists(566));
//myTree.Balance();
//Console.WriteLine("balance: " + myTree.GetBalance());
//Console.WriteLine("Does 56 exist in tree: " + myTree.Exists(56));
for (int i = 0; i < SAMPLE_SIZE; i++)
{
    sample[i] = rng.Next(1,101);
    //sample[i] = i;
}
foreach (var num in sample)
{
    myTree.Insert(num);
}
Console.WriteLine("balance: " + myTree.GetBalance());
Console.WriteLine("count: " + myTree.Count());
Console.WriteLine("Trying to balance");
myTree.Balance();
Console.WriteLine("balance after Balance(): " + myTree.GetBalance());
Console.WriteLine("Done");