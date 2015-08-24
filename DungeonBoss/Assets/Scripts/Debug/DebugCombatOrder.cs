using UnityEngine;
using System.Collections;

public class DebugCombatOrder : MonoBehaviour 
{
	public CombatManager Combat;
	public EventManager Events;
	private int[] NumTurnsPerCharacter;

	void Start()
	{
		//Combat = Managers.GetComponent<CombatManager>();
		//Events = Managers.GetComponent<EventManager>();
	}

	void OnGUI()
	{
		GUILayout.BeginVertical();

		GUILayout.Label("Character Initiatives:");
		for (int i = 0; i < Combat.ManualCharacterArray.Length; i++)
		{
			GUILayout.Label(Combat.ManualCharacterArray[i].name + ": " + Combat.ManualCharacterArray[i].TurnsGiven);
		}

		GUILayout.Label("Upcoming Turns:");
		for (int i = 0; i < Combat.PrecalculatedTurnOrder.Count; i++)
		{
				GUILayout.Label(Combat.PrecalculatedTurnOrder[i].name);
		}

		GUILayout.EndVertical();
	}
}
