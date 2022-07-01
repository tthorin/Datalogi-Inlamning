using Datalogi_Inlamning;

BinarySearchTree<int> myTree = new();
static void TestBalance(BinarySearchTree<int> myTree)
{
    const int SAMPLE_SIZE = 600000;
    const int RANDOM_MAX = 1000000;
    //var sample = new int[SAMPLE_SIZE];
    var rng = new Random();
    for (int i = 0; i < SAMPLE_SIZE; i++)
    {
        var num = rng.Next(1, RANDOM_MAX + 1);
        myTree.Insert(num);
        //myTree.Insert(i+1);
    }
    //myTree.Insert(1);
    //myTree.Insert(2);
    //myTree.Insert(3);
    //myTree.Insert(4);
    //myTree.Insert(5);
    //myTree.Insert(6);
    //myTree.Insert(7);
    Console.WriteLine("Done, balanced "+myTree.balanceTimes + " times during insert.");
    Console.WriteLine("balance after insert complete: " + myTree.GetBalance());
    Console.WriteLine("count: " + myTree.Count());
    Console.WriteLine("max depth after insert: " + myTree.Root.GetMaxDepth());
    myTree.Balance();
    Console.WriteLine("\nBalance again after insert complete, balance is now: " + myTree.GetBalance());
    Console.WriteLine("Max depth: " + myTree.Root.GetMaxDepth());
}
static void TestGetBalance(BinarySearchTree<int> tree)
{
    tree.Insert(50);
    tree.Insert(30);
    tree.Insert(80);
    tree.Insert(20);
    tree.Insert(40);
    tree.Insert(65);
    tree.Insert(62);

    Console.WriteLine("Balance for tree is: "+tree.GetBalance());
}
//TestGetBalance(myTree);
TestBalance(myTree);
//myTree.Print();