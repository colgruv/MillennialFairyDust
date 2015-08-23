using UnityEngine;
using System.Collections.Generic;

public class GridHex : MonoBehaviour 
{
	// Adjacent hexes, array length 6, stored in counterclockwise order (i0 = upper right, i1 = top ... i5 = lower right)
	public GridHex[] AdjacentHexes;
	
	private List<GridEffect> m_Effects;
	public List<GridEffect> Effects { get { return m_Effects; } }
	
	private List<CombatCharacterController> m_Characters;
	public List<CombatCharacterController> Characters { get { return m_Characters; } }
	
	void Start()
	{
		//AdjacentHexes = new GridHex[6];
	}
	
	void Update()
	{
		DeclutterChildren();
	}
	
	public void Place(GridHex _fromHex, int _adjIndex, float _spacing)
	{
		// Determine position offset
		Debug.Log("Determining new hex placement.");
		Vector3 fromHexPosition = _fromHex.transform.position;
		float placeTheta = 30f + (_adjIndex*60f);
		Debug.Log("Place Theta: " + placeTheta);
		float placeThetaR = placeTheta * Mathf.PI / 180f;
		Vector3 positionOffset = new Vector3(Mathf.Cos(placeThetaR)*_spacing, 0.0f, Mathf.Sin(placeThetaR)*_spacing);
		transform.position = fromHexPosition + positionOffset;
		
		// Establish mutual adjacency
		//Debug.Log("Adj index: " + _adjIndex);
		//_fromHex.AdjacentHexes[_adjIndex] = this;
		
		/*
		int oppositeAdjIndex = _adjIndex + 3;
		if (oppositeAdjIndex > 5) oppositeAdjIndex -= 6;
		Debug.Log(AdjacentHexes.Length);
		Debug.Log("Opposite adj index: " + oppositeAdjIndex);
		AdjacentHexes[oppositeAdjIndex] = _fromHex;
		*/
		
		//FindAdjacentHexes(_spacing);
	}
	
	/// <summary>
	/// When a new tile is placed, all hexes need to recalculate their adjacencies
	/// to make sure we don't overlap. There's probably a more elegant way to do this.
	/// </summary>
	/// <param name="_spacing">_spacing.</param>
	public void ProbeAdjacentHexes(List<GridHex> _hexes, float _spacing)
	{
		for (int i = 0; i < 6; i++)
		{
			//if (AdjacentHexes[i] == null)
			//{
			Vector3 fromHexPosition = transform.position;
			float placeTheta = 30f + (i*60f);
			float placeThetaR = placeTheta * Mathf.PI / 180f;
			Vector3 positionOffset = new Vector3(Mathf.Cos(placeThetaR)*_spacing, 0.0f, Mathf.Sin(placeThetaR)*_spacing);
			
			Vector3 adjacentPosition = fromHexPosition + positionOffset;
			
			bool hexFound = false;
			for (int j = 0; j < _hexes.Count; j++)
			{
				if ((_hexes[j].transform.position - adjacentPosition).magnitude < (_spacing/2))
				{
					Debug.Log("Found adjacent hex.");
					AdjacentHexes[i] = _hexes[j];
					hexFound = true;
					Debug.Log("Adjacent hex set: " + AdjacentHexes[i].name);
				}
			}
			if (!hexFound)
			{
				AdjacentHexes[i] = null;
			}
			//}
		}
	}
	
	public bool IsSurrounded()
	{
		for (int i = 0; i < AdjacentHexes.Length; i++)
		{
			if (AdjacentHexes[i] == null)
				return false;
		}
		return true;
	}
	
	private void DeclutterChildren()
	{
		CombatCharacterController[] characterControllers = GetComponentsInChildren<CombatCharacterController>();
		
		int numChildren = characterControllers.Length;
		if (characterControllers.Length == 0)
			return;
		
		float thetaTargetR;
		float distanceFromCenter = 1.0f;
		Vector3 seekPoint;
		
		
		if (characterControllers.Length == 1)
		{
			seekPoint = Vector3.zero;
			Vector3 currentPos = characterControllers[0].transform.localPosition;
			Vector3 direction = seekPoint - currentPos;
			if (direction.magnitude > 0.05f)
				characterControllers[0].transform.Translate(direction.normalized * 
				                                            characterControllers[0].MoveSpeed *
				                                            Time.deltaTime);
		}
		else
		{
			for (int i = 0; i < characterControllers.Length; i++)
			{
				thetaTargetR = (360f / numChildren) * (i+1) * Mathf.PI / 180f;
				seekPoint = new Vector3(Mathf.Cos(thetaTargetR)*distanceFromCenter, Mathf.Sin(thetaTargetR)*distanceFromCenter, 0.0f);
				Vector3 currentPos = characterControllers[i].transform.localPosition;
				Vector3 direction = seekPoint - currentPos;
				if (direction.magnitude > 0.05f)
					characterControllers[i].transform.Translate(direction.normalized * 
					                                            characterControllers[i].MoveSpeed *
					                                            Time.deltaTime);
			}
		}
	}
}
