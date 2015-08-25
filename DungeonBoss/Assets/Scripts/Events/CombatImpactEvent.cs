using UnityEngine;
using System.Collections;

public class CombatImpactEvent : CombatActionEvent 
{
	public CombatCharacterController Attacker;
	public EventManager.UIAction ActionType;

	public override void Init()
	{
		Debug.Log("CombatImpactEvent()");
		CombatCharacterController[] characterControllers = Target.GetComponentsInChildren<CombatCharacterController>();
		for (int i = 0; i < characterControllers.Length; i++)
		{
			switch(ActionType)
			{
			case EventManager.UIAction.ATTACK:
				float damage = calcAttackDamage(Attacker, characterControllers[i]);
				characterControllers[i].GetComponent<Animator>().Play("Character_Impact");
				Debug.Log("Attack damage: " + damage);
				characterControllers[i].HP -= (int)damage;
				Target.transform.FindChild("ImpactEffect").GetComponent<ParticleSystem>().Play();
				break;
			default: 
				break;
			}
		}
		Debug.Log("CombatImpactEvent(): Init finished");
	}

	public override void Process()
	{
		OnEventComplete();
	}

	private float calcAttackDamage(CombatCharacterController _attacker, CombatCharacterController _defender)
	{
		float power = _attacker.Power;
		float defense = _defender.Defense;
		float luck = Random.Range(0.8f, 1.2f);
		float damage = power * (power / (power + defense)) * luck;
		return damage;
	}
}
