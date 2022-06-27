namespace Datalogi_Inlamning;

public class Node<T>
{
	public T Data { get; set; }
	public Node<T>? LeftChild { get; set; }
	public Node<T>? RightChild { get; set; }

	public Node(T value)
	{
		LeftChild = null;
		RightChild = null;
		Data = value;
	}

	// A balanced tree should be as close as possible to equal amount of nodes on both sides
	// 0 is best, but +1 and -1 is ok.
	public int GetBalance()
	{
		int left = (LeftChild == null) ? 0 : LeftChild.GetBalance() + 1;
		int right = (RightChild == null) ? 0 : RightChild.GetBalance() + 1;
		return right - left;
	}
}
