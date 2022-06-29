namespace Datalogi_Inlamning;

using Datalogi_Inlamning.Interfaces;

public class BinarySearchTree<T> : IBstG<T>, IBstVg<T> where T : IComparable<T>
{
    private Node<T>? Root = null;
    private int _count = 0;

    public void Balance()
    {
        var balance = Root?.GetBalance() ?? 0;
        if (Math.Abs(balance) > 1) Balance(balance);
    }
    public void Balance(int balance, int oldBalance = 0, int count = 0)
    {
        if (balance > 1)
            ShiftRootRight();
        else if (balance < 1)
            ShiftRootLeft();
        var newBalance = Root!.GetBalance();
        count = newBalance == oldBalance ? ++count : 0;
        if (Math.Abs(newBalance) > 1 && count < 5) Balance(newBalance, balance, count);
        else if (count >= 5 && Math.Abs(newBalance) > Math.Abs(balance)) Balance(newBalance, balance, count);
    }

    private void ShiftRootLeft()
    {
        var willBeNewRoot = Root!.LeftChild;
        Root.LeftChild = willBeNewRoot!.RightChild;
        willBeNewRoot.RightChild = Root;
        Root = willBeNewRoot;
    }
    private void ShiftRootRight()
    {
        var newRoot = Root!.RightChild;
        Root.RightChild = newRoot!.LeftChild;
        newRoot.LeftChild = Root;
        Root = newRoot;
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
                InsertSame(currentNode, newNode);
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
                currentNode = currentNode.RightChild;
            }
        }
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
