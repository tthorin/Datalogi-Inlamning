namespace Datalogi_Inlamning;

using Datalogi_Inlamning.Interfaces;

public class BinarySearchTree<T> : IBstG<T>, IBstVg<T> where T : IComparable<T>
{
    //todo: change Root back to private
    public Node<T>? Root = null;
    private int _count = 0;
    private int _balance = 0;
    public int balanceTimes = 0;
    public bool rebalanced = false;
    public void Balance()
    {
        var balance = Root?.GetBalance() ?? 0;
        if (Math.Abs(balance) > 1) Balance(Root, null, false, balance);
    }
    public void Balance(Node<T> nodeToBalance, Node<T> parent, bool isLeftChild, int balance, int oldBalance = 0, int count = 0)
    {
        balanceTimes++;
        if (balance > 1)
            nodeToBalance = ShiftRootRight(nodeToBalance, parent, isLeftChild);
        else if (balance < 1)
            nodeToBalance = ShiftRootLeft(nodeToBalance, parent, isLeftChild);
        //_balance = Root!.GetBalance();
        var newBalance = nodeToBalance!.GetBalance();
        count = newBalance == oldBalance ? ++count : 0;
        if (Math.Abs(newBalance) > 1 && count < 5) Balance(nodeToBalance, parent, isLeftChild, newBalance, balance, count);
        Console.WriteLine(Math.Ceiling(Math.Log2(_count)) +" "+ Root.GetMaxDepth());
        if ((count == 5 && nodeToBalance == Root) || (Math.Ceiling(Math.Log2(_count)+2))<Root.GetMaxDepth()) RebalanceTree(Root);
        //else if (count >= 5 && Math.Abs(newBalance) > Math.Abs(balance)) Balance(nodeToBalance, newBalance, balance, count);
    }
    public void RebalanceTree(Node<T> node, Node<T> parent = null!, bool isLeftChild = true)
    {
        if (!rebalanced) rebalanced = true;
        if (node.LeftChild is not null)
        {
            RebalanceTree(node.LeftChild, node, true);
            var balance = node.LeftChild.GetBalance();
            if (Math.Abs(balance) > 1) Balance(node.LeftChild, node, isLeftChild, balance);
        }

        if (node.RightChild is not null)
        {
            RebalanceTree(node.RightChild, node, false);
            var balance = node.RightChild.GetBalance();
            if (Math.Abs(balance) > 1) Balance(node.RightChild, node, isLeftChild, balance);
        }
    }

    private Node<T> ShiftRootLeft(Node<T> node, Node<T> parent, bool isLeftChild)
    {
        var willBeNewRoot = node!.LeftChild;
        node.LeftChild = willBeNewRoot!.RightChild;
        willBeNewRoot.RightChild = node;
        //node = willBeNewRoot;
        if (parent is null) Root = willBeNewRoot;
        else if (isLeftChild) parent.LeftChild = willBeNewRoot;
        else parent.RightChild = willBeNewRoot;
        return willBeNewRoot;
    }
    private Node<T> ShiftRootRight(Node<T> node, Node<T> parent, bool isLeftChild)
    {
        var newRoot = node!.RightChild;
        node.RightChild = newRoot!.LeftChild;
        newRoot.LeftChild = node;
        //node = newRoot;
        if (parent is null) Root = newRoot;
        else if (isLeftChild) parent.LeftChild = newRoot;
        else parent.RightChild = newRoot;
        return newRoot;
    }

    //TODO: remove before check-in, only for testing
    internal int GetBalance() => Root?.GetBalance() ?? 0;

    public int Count() => _count;

    public bool Exists(T value)
    {
        var currentNode = Root;
        while (currentNode != null)
        {
            var compareValue = currentNode.Data.CompareTo(value);
            if (compareValue == 0) return true;
            else if (compareValue > 0)
                currentNode = currentNode.LeftChild;
            else currentNode = currentNode.RightChild;
        }
        return false;
    }

    public void Insert(T value)
    {
        var newNode = new Node<T>(value);
        if (Root is null)
        {
            Root = newNode;
            _count++;
            return;
        }
        var currentNode = Root;
        var nodeNotInserted = true;
        while (nodeNotInserted)
        {
            var compareValue = newNode.Data.CompareTo(currentNode!.Data);
            if (compareValue == 0)
            {
                //InsertSame(currentNode, newNode);
                nodeNotInserted = false;
            }
            else if (compareValue < 0)
            {
                if (currentNode.LeftChild == null)
                {
                    currentNode.LeftChild = newNode;
                    _count++;
                    nodeNotInserted = false;
                }
                if (currentNode == Root) _balance--;
                currentNode = currentNode.LeftChild;
            }
            else
            {
                if (currentNode.RightChild == null)
                {
                    currentNode.RightChild = newNode;
                    _count++;
                    nodeNotInserted = false;
                }
                if (currentNode == Root) _balance++;
                currentNode = currentNode.RightChild;
            }
        }
        if((Math.Ceiling(Math.Log2(_count) + 1)) < Root.GetMaxDepth()) RebalanceTree(Root);
        var temp = Root.GetBalance();
        if (Math.Abs(temp) > 1) Balance(Root, null, false, temp);
    }

    private void InsertSame(Node<T> currentNode, Node<T> nodeToInsert)
    {
        if (currentNode.LeftChild is null)
        {
            currentNode.LeftChild = nodeToInsert;
            _count++;
        }
        else if (currentNode.RightChild is null)
        {
            currentNode.RightChild = nodeToInsert;
            _count++;
        }
        else
        {
            var balance = currentNode.GetBalance();
            if (balance > 0) InsertSame(currentNode.LeftChild, nodeToInsert);
            else InsertSame(currentNode.RightChild, nodeToInsert);
        }
    }

    public void Remove(T value)
    {
        throw new NotImplementedException();
    }
    public void Print()
    {
        Queue<Node<T>?> nodes = new Queue<Node<T>?>();
        Queue<Node<T>?> newNodes = new Queue<Node<T>?>();
        nodes.Enqueue(Root);
        int depth = 0;

        bool exitCondition = false;
        while (nodes.Count > 0 && !exitCondition)
        {
            depth++;
            newNodes = new Queue<Node<T>?>();

            string xs = "[";
            foreach (var maybeNode in nodes)
            {
                string data = maybeNode == null ? " " : "" + maybeNode.Data;
                if (maybeNode == null)
                {
                    xs += "_, ";
                    newNodes.Enqueue(null);
                    newNodes.Enqueue(null);
                }
                else
                {
                    Node<T> node = maybeNode;
                    string s = node.Data.ToString();
                    xs += s.Substring(0, Math.Min(4, s.Length)) + ", ";
                    if (node.LeftChild != null) newNodes.Enqueue(node.LeftChild);
                    else newNodes.Enqueue(null);
                    if (node.RightChild != null) newNodes.Enqueue(node.RightChild);
                    else newNodes.Enqueue(null);
                }
            }
            xs = xs.Substring(0, xs.Length - 2) + "]";

            Console.WriteLine(xs);

            nodes = newNodes;
            exitCondition = true;
            foreach (var m in nodes)
            {
                if (m != null) exitCondition = false;
            }
        }
    }
}
