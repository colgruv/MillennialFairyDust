using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GridManager : MonoBehaviour 
{
	[Header("Object Pointers")]
	public GameObject HexPrefab;
	public GameObject CenterHex;
	public GameObject HexCanvas;

	[Header("Grid Procedural Properties")]
	public int MinHexes;
	public int MaxHexes;
	public float GridSpacing;

	[Header("Debug Attributes")]
	public GridHex UniversalNPCTarget;

	private List<GridHex> m_GridHexes;
	public List<GridHex> GridHexes { get { return m_GridHexes; } }

	// Use this for initialization
	void Awake () 
	{
		GenerateHexes();

		Pathfinding.PathToHex(m_GridHexes[0], m_GridHexes[m_GridHexes.Count-1], 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void GenerateHexes()
	{
		m_GridHexes = new List<GridHex>();
		m_GridHexes.Add(CenterHex.GetComponent<GridHex>());

		int numHexes = Random.Range(MinHexes, MaxHexes);
		for (int i = 0; i < numHexes; i++)
		{
			AddHex(i);
		}
	}

	private void AddHex(int _index)
	{
		// Find list of viable, non-surrounded hexes
		List<GridHex> nonSurroundedHexes = new List<GridHex>();
		for (int i = 0; i < m_GridHexes.Count; i++)
		{
			if (!m_GridHexes[i].IsSurrounded())
				nonSurroundedHexes.Add(m_GridHexes[i]);
		}
		int fromHexIndex = Random.Range(0, nonSurroundedHexes.Count - 1);
		GridHex fromHex = nonSurroundedHexes[fromHexIndex];

		//Find list of directions in which the non-surrounded hex does *not* have an adjacent hex.
		List<int> emptyDirections = new List<int>();
		for (int i = 0; i < 6; i++)
		{
			if (fromHex.AdjacentHexes[i] == null)
				emptyDirections.Add(i);
		}
		int newHexDirectionIndex = Random.Range(0, emptyDirections.Count - 1);
		int newHexDirection = emptyDirections[newHexDirectionIndex];

		// Instantiate new Hex
		GameObject newGridHexGO = GameObject.Instantiate(HexPrefab);
		newGridHexGO.name = "Hex_" + (_index+1).ToString();
		newGridHexGO.transform.SetParent(HexCanvas.transform);
		newGridHexGO.transform.localRotation = Quaternion.identity;
		GridHex newGridHex = newGridHexGO.GetComponent<GridHex>();

		// Place the new Hex in relation to FromHex
		newGridHex.Place(fromHex, newHexDirection, GridSpacing);
		m_GridHexes.Add(newGridHex);

		// Have all hexes probe for new adjacent hexes.
		foreach (GridHex hex in m_GridHexes)
		{
			hex.ProbeAdjacentHexes(m_GridHexes, GridSpacing);
		}
	}

	public void OnUniversalNPCTargetClicked(GridHex _hexTarget)
	{
		UniversalNPCTarget = _hexTarget;
	}
}
