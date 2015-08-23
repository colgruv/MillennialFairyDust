using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The Event Manager stores a Queue of Event objects and processes them in turn.
/// Works closely with the CombatTurnOrder class?
/// </summary>
public class EventManager : MonoBehaviour 
{
	private Queue<CombatEvent> m_QueuedEvents;
	private CombatManager m_CombatManager;

	private CombatEvent m_CurrentCombatEvent;
	public CombatEvent CurrentEvent { get { return m_CurrentCombatEvent; } }
	private CombatEvent m_PriorityEvent;

	void Awake()
	{
		Debug.Log("EventManager::Start()");
		m_QueuedEvents = new Queue<CombatEvent>();
		m_CombatManager = GetComponent<CombatManager>();
	}

	void OnEnable()
	{
		CombatEvent.EventComplete += GetNextEvent;
		CombatEvent.QueueEvent += QueueEvent;
	}

	void OnDisable()
	{
		CombatEvent.EventComplete -= GetNextEvent;
		CombatEvent.QueueEvent -= QueueEvent;
	}

	void Update()
	{
		if (m_CurrentCombatEvent == null)
		{ // If there are no events (first update) get an event from the combat manager.
			// This should not happen after the first update.
			//Debug.Log("Getting first event");
			GetNextEvent();
		}
		else
		{ // If we already have a current event, process it.
			//Debug.Log("Processing current event");
			m_CurrentCombatEvent.Process();
		}
	}

	void GetNextEvent()
	{
		Debug.Log("GetNextEvent()");
		if (m_PriorityEvent != null) 
		{
			m_CurrentCombatEvent = m_PriorityEvent;
			m_PriorityEvent = null;
		} 
		else if (m_QueuedEvents.Count > 0)
		{
			m_CurrentCombatEvent = m_QueuedEvents.Dequeue();
		}
		else
		{
			m_CurrentCombatEvent = m_CombatManager.GetNextCharacterTurn();
		}

		Debug.Log("Initializing next event.");
		m_CurrentCombatEvent.Init();
	}
	
	public void QueueEvent(CombatEvent _combatEvent)
	{
		m_QueuedEvents.Enqueue(_combatEvent);
	}


	/// <summary>
	/// Add an event immediately after the current event.
	/// Useful for chaining events that are contingent on the current event, such as action and then target selection.
	/// </summary>
	/// <param name="_combatEvent">_combat event.</param>
	public void QueuePriorityEvent(CombatEvent _combatEvent)
	{
		if (m_PriorityEvent != null) 
			Debug.LogWarning("Overwriting previous priority event.");

		m_PriorityEvent = _combatEvent;
	}
}
