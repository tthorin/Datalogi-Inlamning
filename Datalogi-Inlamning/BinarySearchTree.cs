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
    public void Balance(int balance)
    {
        if (balance > 1)
            ShiftRight();
        else if (balance < 1)
            ShiftLeft();
        if (Math.Abs(balance) > 1) Balance(balance);
    }

    private void ShiftRight()
    {
        var currentNode = Root.RightChild;
        //if (currentNode.RightChild.LeftChild == null)
        //{
        //    currentNode.RightChild.LeftChild = currentNode;
        //    Root = currentNode.RightChild;
        //    currentNode.RightChild = null;
        //}
        //else
        //{
        //    currentNode = currentNode.RightChild;
        //}
        var notMovedYet = true;
        while (notMovedYet)
        {
            if (currentNode.LeftChild == null)
            {
                var temp = Root;
                currentNode.LeftChild = Root;

                Root = temp.RightChild;
                temp.RightChild = null;
                notMovedYet = false;
            }
            else
            {
                currentNode = currentNode.LeftChild;
            }
        }
    }
    private void ShiftLeft()
    {
        var currentNode = Root.LeftChild;
        //if (currentNode.LeftChild.RightChild == null)
        //{
        //    currentNode.LeftChild.RightChild = currentNode;
        //    Root = currentNode.LeftChild;
        //    currentNode.LeftChild = null;
        //}
        //else
        //{
        //    currentNode = currentNode.LeftChild.RightChild;
        //}
        var notMovedYet = true;
        while (notMovedYet)
        {
            if (currentNode.RightChild == null)
            {
                var temp = Root;
                currentNode.RightChild = Root;

                Root = temp.LeftChild;
                temp.LeftChild = null;
                notMovedYet = false;
            }
            else
            {
                currentNode = currentNode.RightChild;
            }
        }
    }

    //TODO: remove before check-in, only for testing
    internal int GetBalance() => Root.GetBalance();

    public int Count() => _count;

    public bool Exists(T value)
    {
        var currentNode = Root;
        while (currentNode != null)
        {
            if (currentNode.Data.CompareTo(value) == 0) return true;
            else if (currentNode.Data.CompareTo(value) > 0)
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
        while (true)
        {
            if (newNode.Data.CompareTo(currentNode!.Data) == 0)
            {
                InsertSame(currentNode, newNode);
                return;
            }
            else if (newNode.Data.CompareTo(currentNode.Data) < 0)
            {
                if (currentNode.LeftChild == null)
                {
                    currentNode.LeftChild = newNode;
                    _count++;
                    return;
                }
                currentNode = currentNode.LeftChild;
            }
            else
            {
                if (currentNode.RightChild == null)
                {
                    currentNode.RightChild = newNode;
                    _count++;
                    return;
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
}
