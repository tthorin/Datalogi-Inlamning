using Datalogi_Inlamning;

BinarySearchTree<int> myTree = new();
static void TestBalance(BinarySearchTree<int> myTree)
{
    const int SAMPLE_SIZE = 100;
    const int RANDOM_MAX = 100;
    var sample = new int[SAMPLE_SIZE];
    var rng = new Random();
    for (int i = 0; i < SAMPLE_SIZE; i++)
    {
        sample[i] = rng.Next(1, RANDOM_MAX + 1);
    }
    foreach (var num in sample)
    {
        myTree.Insert(num);
    }
    Console.WriteLine("balance: " + myTree.GetBalance());
    Console.WriteLine("count: " + myTree.Count());
    //Console.WriteLine("Trying to balance");
    //myTree.Balance();
    //Console.WriteLine("balance after Balance(): " + myTree.GetBalance());
    Console.WriteLine("Done, balanced "+myTree.balanceTimes + " times.");
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