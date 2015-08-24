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
			int distanceToHex = Pathfinding.PathToHex(characterHex, EventManager.SelectedHex, Character.Size).Count - 1;
			Debug.Log("Distance to selected hex: " + distanceToHex);

			if (distanceToHex == Character.Movement)
			{
				switch(Action)
				{
				case EventManager.UIAction.MOVE:
					CombatMoveEvent moveEvent = new CombatMoveEvent();
					moveEvent.Character = Character;
					moveEvent.FinalTarget = EventManager.SelectedHex;
					OnQueueEvent(moveEvent);
					break;
				case EventManager.UIAction.ATTACK:
					Debug.Log("Attack not implemented yet.");
					break;
				}

				OnEventComplete();
			}

			EventManager.SetSelectedHex(null);
			GetPanel("UI_PromptTarget").gameObject.SetActive(true);
		}
	}
}
