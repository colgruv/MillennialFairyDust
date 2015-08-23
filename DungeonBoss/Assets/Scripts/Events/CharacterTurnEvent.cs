using UnityEngine;
using System.Collections;

public class CharacterTurnEvent : CharacterControllerEvent 
{
	private float m_TimeSinceStartup;
	public float StartupDelay = 1.0f;

	public override void Init()
	{
		Debug.Log("BeginCharacterTurnEvent::Init()");

		m_TimeSinceStartup = 0.0f;
	}

	public override void Process()
	{
		//Debug.Log("BeginCharacterTurnEvent::Process()");

		m_TimeSinceStartup += Time.deltaTime;
		if (m_TimeSinceStartup >= StartupDelay)
		{
			Debug.Log("Invoking OnEventComplete()");
			OnEventComplete();
		}
	}
}
