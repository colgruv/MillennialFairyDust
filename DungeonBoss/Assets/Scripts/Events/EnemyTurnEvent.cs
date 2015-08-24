using UnityEngine;
using System.Collections.Generic;

public class EnemyTurnEvent : CharacterTurnEvent 
{
	GridManager m_GridManager;

	// Use this for initialization
	public override void Init()
	{
		CombatMoveEvent moveEvent = new CombatMoveEvent();
		moveEvent.Character = Character;

		m_GridManager = GameObject.FindObjectOfType<GridManager>();
		GridHex targetHex = m_GridManager.UniversalNPCTarget;
		GridHex currentHex = Character.transform.parent.gameObject.GetComponent<GridHex>();

		if (currentHex != targetHex && targetHex.AvailableSpace >= 2)
		{
			moveEvent.FinalTarget = Pathfinding.PathToHex(currentHex, targetHex)[1];
			OnQueueEvent(moveEvent);
		}

		/* // Random hex movement
		GridHex currentHex = Character.transform.parent.gameObject.GetComponent<GridHex>();
		List<GridHex> availableHexes = new List<GridHex>();
		for (int i = 0; i < 6; i++)
		{
			if (currentHex.AdjacentHexes[i] != null)
				availableHexes.Add(currentHex.AdjacentHexes[i]);
		}

		int targetHexIndex = Random.Range(0, availableHexes.Count - 1);
		moveEvent.FinalTarget = availableHexes[targetHexIndex];
		*/


	}
	
	public override void Process()
	{
		OnEventComplete();
	}
}
