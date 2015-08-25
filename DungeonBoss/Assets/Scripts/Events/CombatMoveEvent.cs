using UnityEngine;
using System.Collections;

public class CombatMoveEvent : CombatActionEvent 
{
	public GridHex FinalTarget;

	// TODO: Pathfinding
	private GridHex m_currentTarget; 
	private GridHex m_startingPoint;

	// Use this for initialization
	public override void Init() 
	{
		//Vector3 direction = (FinalTarget.transform.position - Character.transform.position).normalized;
		//Character.transform.up = direction;
		m_startingPoint = Character.transform.parent.gameObject.GetComponent<GridHex>();
		Character.transform.SetParent(FinalTarget.transform);
	}
	
	// Update is called once per frame
	public override void Process() 
	{
		Vector3 direction = Character.transform.localPosition * -1;

		//Debug.Log(direction.magnitude);
		if (direction.magnitude <= 1.0f)
		{
			OnEventComplete();
		}
		else
		{
			direction.Normalize();
			Character.transform.Translate(direction * Character.MoveSpeed * Time.deltaTime);
		}
	}

	protected virtual void OnTargetReached()
	{
		OnEventComplete();
	}
}
