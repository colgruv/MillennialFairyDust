using UnityEngine;
using System.Collections.Generic;

public class EnemyTurnEvent : CharacterTurnEvent 
{
	// Use this for initialization
	public override void Init()
	{
		CombatMoveEvent moveEvent = new CombatMoveEvent();
		moveEvent.Character = Character;

		Debug.Log("From: " + Character.transform.parent.gameObject.name);
		GridHex currentHex = Character.transform.parent.gameObject.GetComponent<GridHex>();
		List<GridHex> availableHexes = new List<GridHex>();
		for (int i = 0; i < 6; i++)
		{
			if (currentHex.AdjacentHexes[i] != null)
				availableHexes.Add(currentHex.AdjacentHexes[i]);
		}

		int targetHexIndex = Random.Range(0, availableHexes.Count - 1);
		moveEvent.FinalTarget = availableHexes[targetHexIndex];
		Debug.Log("To: " + moveEvent.FinalTarget.gameObject.name);

		OnQueueEvent(moveEvent);
	}
	
	public override void Process()
	{
		OnEventComplete();
	}
}
