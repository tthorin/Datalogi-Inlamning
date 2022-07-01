using Datalogi_Inlamning;

BinarySearchTree<int> myTree = new();
static void TestBalance(BinarySearchTree<int> myTree)
{
    const int SAMPLE_SIZE = 700000;
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
    Console.WriteLine("balance: " + myTree.GetBalance());
    Console.WriteLine("count: " + myTree.Count());
    //Console.WriteLine("Trying to balance");
    //myTree.Balance();
    //Console.WriteLine("balance after Balance(): " + myTree.GetBalance());
    Console.WriteLine("Done, balanced "+myTree.balanceTimes + " times.");
    Console.WriteLine("max depth: " + myTree.Root.GetMaxDepth());
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