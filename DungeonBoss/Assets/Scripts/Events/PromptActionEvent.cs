using UnityEngine;
using System.Collections;

public class PromptActionEvent : CombatPromptEvent 
{
	public override void Init()
	{	
		Debug.Log("PromptActionEvent()");

		GetPanel("UI_PromptAction").gameObject.SetActive(true);

		Debug.Log("Prompt Action Event(): Init finished.");
	}

	public override void Process()
	{
		if (EventManager.CurrentUIAction != EventManager.UIAction.NONE)
		{
			PromptTargetEvent targetEvent = new PromptTargetEvent();
			targetEvent.Character = Character;
			targetEvent.Action = EventManager.CurrentUIAction;
			OnQueueEvent(targetEvent);

			EventManager.CurrentUIAction = EventManager.UIAction.NONE;
			GetPanel("UI_PromptAction").gameObject.SetActive(false);

			OnEventComplete();
		}
	}
}
