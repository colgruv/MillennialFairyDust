using UnityEngine;
using System.Collections;

public class PromptTargetEvent : CombatPromptEvent 
{
	public EventManager.UIAction Action;
	private GridHex m_Target;

	public override void Init()
	{
		Debug.Log("PromptTargetEvent()");

		GetPanel("UI_PromptTarget").gameObject.SetActive(true);

		Debug.Log("PromptTargetEvent(): Init finished.");
	}

	public override void Process()
	{
		if (EventManager.SelectedHex != null)
		{
			Debug.Log("Selected HEX detected.");

			GridHex characterHex = Character.transform.parent.GetComponent<GridHex>();
			Debug.Log("Character Hex: " + characterHex);
			int distanceToHex;

			switch(Action)
			{
			case EventManager.UIAction.MOVE:
				distanceToHex = Pathfinding.PathToHex(characterHex, EventManager.SelectedHex, Character.Size).Count - 1;

				if (distanceToHex == 1)
				{
					CombatMoveEvent moveEvent = new CombatMoveEvent();
					moveEvent.Character = Character;
					moveEvent.FinalTarget = EventManager.SelectedHex;
					OnQueueEvent(moveEvent);
				}

				OnEventComplete();
				Debug.Log("PromptTargetEvent complete");
				break;
			case EventManager.UIAction.ATTACK:
				distanceToHex = Pathfinding.PathToHex(characterHex, EventManager.SelectedHex, 0).Count - 1;

				if (distanceToHex == 1)
				{
					Debug.Log("CombatAttackEvent Triggered.");
					CombatAttackEvent attackEvent = new CombatAttackEvent();
					attackEvent.Character = Character;
					attackEvent.Target = EventManager.SelectedHex;
					OnQueueEvent(attackEvent);
					Debug.Log("CombatAttackEvent Queued");
				}

				OnEventComplete();
				Debug.Log("PromptTargetEvent complete");
				break;
			}

			EventManager.SetSelectedHex(null);
			GetPanel("UI_PromptTarget").gameObject.SetActive(true);

			Debug.Log("EndProcess PromptTargetEvent()");
		}
	}
}
