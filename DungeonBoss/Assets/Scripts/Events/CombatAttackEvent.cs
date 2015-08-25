using UnityEngine;
using System.Collections;

public class CombatAttackEvent : CombatActionEvent 
{
	private const float ATTACK_DELAY = 1.0f;
	private float m_InitTime;

	public override void Init()
	{
		Debug.Log("CombatActionEvent()");
		//Vector3 direction = (Target.transform.position - Character.transform.position).normalized;
		//Character.transform.up = direction;
		Character.GetComponent<Animator>().Play("Character_Attack");
		m_InitTime = Time.time;
		Debug.Log("CombatActionEvent(): Init finished");
	}

	public override void Process()
	{
		if (Time.time - m_InitTime > ATTACK_DELAY)
		{
			Debug.Log("ImpactEvent triggered.");
			CombatImpactEvent impactEvent = new CombatImpactEvent();
			impactEvent.Attacker = Character;
			impactEvent.ActionType = EventManager.UIAction.ATTACK;
			impactEvent.Target = Target;
			impactEvent.Character = Character;
			OnQueueEvent(impactEvent);
			OnEventComplete();
		}
	}
}
