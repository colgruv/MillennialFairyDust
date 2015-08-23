using UnityEngine;
using System.Collections.Generic;

public abstract class CombatEvent 
{
	public delegate void EventCompleteAction();
	public static event EventCompleteAction EventComplete;
	protected virtual void OnEventComplete()
	{
		EventCompleteAction handler = EventComplete; 
		if (handler != null)
			handler();
	}

	public delegate void QueueEventAction(CombatEvent _event);
	public static event QueueEventAction QueueEvent;
	protected virtual void OnQueueEvent(CombatEvent _event)
	{
		QueueEventAction handler = QueueEvent;
		if (handler != null)
			handler(_event);
	}

	public abstract void Init();

	// Update is called once per frame
	public abstract void Process();
}
