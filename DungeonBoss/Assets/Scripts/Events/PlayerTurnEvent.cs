using UnityEngine;
using System.Collections;

public class PlayerTurnEvent : CharacterTurnEvent 
{
	public override void Init()
	{
		Debug.Log("PlayerTurnEvent()");

		PromptActionEvent promptEvent = new PromptActionEvent();
		promptEvent.Character = Character;
		OnQueueEvent(promptEvent);

		Debug.Log("PlayerTurnEvent(): Init finished.");
	}

	public override void Process()
	{
		OnEventComplete();
	}
}
