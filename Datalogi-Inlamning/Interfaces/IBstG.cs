namespace Datalogi_Inlamning.Interfaces;

public interface IBstG<T> where T : IComparable<T>
{
	// Remember: the most efficient tree is a balanced tree. A balanced tree has the same (or as close as possible to) amount of nodes on the left as on the right.

	// Inserts a new value to the tree
	public void Insert(T value);

	// Returns true if an object that is equal to value exists in the tree
	// Uses the IComparable<T> interface. x.CompareTo(y) == 0
	public bool Exists(T value);

	// Returns the number of objects currently in the tree
	public int Count();
}
