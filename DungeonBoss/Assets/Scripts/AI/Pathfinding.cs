using UnityEngine;
using System.Collections.Generic;

public class Pathfinding
{
	public static List<GridHex> PathToHex(GridHex _startHex, GridHex _endHex, int _moverSize = 2)
	{
		List<List<GridHex>> availablePaths = new List<List<GridHex>>();
		List<GridHex> startingPath = new List<GridHex>();
		startingPath.Add(_startHex);
		availablePaths.Add(startingPath);

		availablePaths = recurseHexPath(_startHex, _endHex, _moverSize, availablePaths);

		//Debug.Log("Number of paths discovered: " + availablePaths.Count);

		// Find shortest path
		int shortestLength = 0;
		int shortestPathIndex = 0;
		for (int i = 0; i < availablePaths.Count; i++)
		{
			string path = "";
			for (int j = 0; j < availablePaths[i].Count; j++)
			{
				path += "-> " + availablePaths[i][j].name;
			}

			//Debug.Log("Path " + i + ": Length " + availablePaths[i].Count + ": " + path);
			if ((shortestLength == 0 || shortestLength > availablePaths[i].Count) && availablePaths[i].Count != 0)
			{
				shortestLength = availablePaths[i].Count;
				shortestPathIndex = i;
			}
		}

		string npath = "";
		for (int j = 0; j < availablePaths[shortestPathIndex].Count; j++)
		{
			npath += "-> " + availablePaths[shortestPathIndex][j].name;
		}
		//Debug.Log("Shortest path has length " + availablePaths[shortestPathIndex].Count + " (" + npath + ")");
		return availablePaths[shortestPathIndex];
	}

	private static List<List<GridHex>> recurseHexPath(GridHex _startHex, GridHex _endHex, int _moverSize, List<List<GridHex>> _paths)
	{
		// The current hex is the last hex on the last path in the list.
		List<GridHex> currentPath = _paths[_paths.Count-1];
		GridHex currentHex = currentPath[currentPath.Count-1];

		if (currentHex == _endHex)
		{
			// Path finished (successfully)
			List<GridHex> newPath = new List<GridHex>(currentPath);
			newPath.Remove(currentHex);
			_paths.Add(newPath);
			//Debug.Log("FOUND SUCCESSFULL PATH OF LENGTH " + currentPath.Count + 
			          //". Saving and branching to new path starting from " + newPath[newPath.Count-1].name + ".");
			return _paths;
		}

		for (int i = 0; i < currentHex.AdjacentHexes.Length; i++)
		{
			currentPath = _paths[_paths.Count-1];
			GridHex nextHex = currentHex.AdjacentHexes[i];
			if (nextHex != null)
			{
				if (nextHex.IsNavigable &&
				    nextHex.AvailableSpace >= _moverSize && 
				    !currentPath.Contains(nextHex))
				{
					//Debug.Log("Moving to next valid hex: " + nextHex.name);
					_paths[_paths.Count-1].Add(nextHex);
					_paths = recurseHexPath(_startHex, _endHex, _moverSize, _paths);
				}
				else
				{
					//Debug.Log("Found invalid hex " + "(" + nextHex.name  + ".)" + "Moving back to " + currentHex.name);
					string path = "";
					for (int j = 0; j < currentPath.Count; j++)
					{
						path += "-> " + currentPath[j].name;
					}
					//Debug.Log("Current path: " + path);
				}
			}
			else
			{
				//Debug.Log("No hex in this direction.");
			}
		}

		// Exhausted all options for this hex.
		//Debug.Log("Exhausted all options for " + currentHex.gameObject.name + ". Removing from current path.");
		_paths[_paths.Count-1].Remove(currentHex);
		if (_paths[_paths.Count-1].Count == 0)
			_paths.Remove(_paths[_paths.Count-1]);
		return _paths;
	}
}
