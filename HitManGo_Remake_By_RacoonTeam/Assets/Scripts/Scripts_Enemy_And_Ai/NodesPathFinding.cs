using System.Collections.Generic;

public class NodesPathFinding
{
	public List<Node> Depth_First_Search(Node start, Node end)
	{
		Stack<Node> work = new Stack<Node>();
		List<Node> visited = new List<Node>();

		work.Push(start);
		visited.Add(start);

		start.history = new List<Node>();

		while (work.Count > 0)
		{

			Node current = work.Pop();
			if (current == end)
			{
				List<Node> result = current.history;
				result.Add(current);
				return result;
			}
			else
			{
				for (int i = 0; i < current.linkedNodes.Count; i++)
				{

					Node currentChild = current.linkedNodes[i];
					if (!visited.Contains(currentChild))
					{
						work.Push(currentChild);
						visited.Add(currentChild);
						currentChild.history = new List<Node>(current.history);
						currentChild.history.Add(current);
					}
				}
			}
		}
		return null;
	}
}
