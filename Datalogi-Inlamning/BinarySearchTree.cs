namespace Datalogi_Inlamning;

using Datalogi_Inlamning.Interfaces;

public class BinarySearchTree<T> : IBstG<T>, IBstVg<T> where T : IComparable<T>
{
    //todo: change Root back to private
    public Node<T>? Root = null;

    private int _count = 0;
    //todo: remove balanceTimes
    public int balanceTimes = 0;
    private int _leftDepth = 0;
    private int _rightDepth = 0;
    public void Balance()
    {
        if (Root is null) return;
        balanceTimes++;
        List<Node<T>> nodes = new();
        StoreTree(Root, nodes);
        Root = RebuildTree(nodes, 0, nodes.Count - 1);
        var depth = Root!.GetMaxDepth();
        _leftDepth = depth;
        _rightDepth = depth;
    }
    private void StoreTree(Node<T> node, List<Node<T>> nodes)
    {
        if (node is null) return;
        StoreTree(node.LeftChild!, nodes);
        nodes.Add(node);
        StoreTree(node.RightChild!, nodes);
    }
    private Node<T>? RebuildTree(List<Node<T>> nodes, int start, int end)
    {
        if (start > end) return null;
        var mid = (start + end) / 2;

        var node = nodes[mid];
        node.LeftChild = RebuildTree(nodes, start, mid - 1);
        node.RightChild = RebuildTree(nodes, mid + 1, end);

        return node;
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
        var depth = 0;
        var nodeNotInserted = true;
        bool onLeftSide = false;
        while (nodeNotInserted)
        {
            depth++;
            var compareValue = newNode.Data.CompareTo(currentNode!.Data);
            if (compareValue == 0)
            {
                nodeNotInserted = false;
            }
            else if (compareValue < 0)
            {
                if (currentNode == Root) onLeftSide = true;
                if (currentNode.LeftChild == null)
                {
                    currentNode.LeftChild = newNode;
                    _count++;
                    nodeNotInserted = false;
                    if (depth > _leftDepth && onLeftSide) _leftDepth = depth;
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
                    if (depth > _rightDepth && !onLeftSide) _rightDepth = depth;
                }
                currentNode = currentNode.RightChild;
            }
        }

        var maxWantedDepth = Math.Ceiling(Math.Log2(_count) + 3);
        if (maxWantedDepth > 10) maxWantedDepth -= maxWantedDepth / 10;
        var biggestDepth = _rightDepth > _leftDepth ? _rightDepth : _leftDepth;
        var balance = _rightDepth - _leftDepth;
        if (_count > 13)
        {
            Root = Root;
        }
        if (Math.Abs(balance) > 1 || biggestDepth > maxWantedDepth) Balance();
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
#pragma warning disable S1643 // Strings should not be concatenated using '+' in a loop
                    xs += "_, ";
                    newNodes.Enqueue(null);
                    newNodes.Enqueue(null);
                }
                else
                {
                    Node<T> node = maybeNode;
                    string s = node.Data.ToString();
                    xs += s.Substring(0, Math.Min(4, s.Length)) + ", ";
#pragma warning restore S1643 // Strings should not be concatenated using '+' in a loop
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
