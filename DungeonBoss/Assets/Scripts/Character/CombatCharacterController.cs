using UnityEngine;
using System.Collections;

public class CombatCharacterController : MonoBehaviour 
{
	public enum ControlType
	{
		NPC,
		PC
	}
	public ControlType CharacterControlType;

	[Header("Basic Info")]
	public int Movement;
	public int Level;
	public int MoveSpeed;
	
	// Maximum attributes
	[Header("Scalable Attributes")]
	public int HP;
	public int Power;
	public int Speed;
	public int Defense;
	public int Resistance;
	
	// Current attributes (including loss and modification)
	private int m_CurrentHP;
	private int m_CurrentPower;
	private int m_CurrentSpeed;
	private int m_CurrentDefense;
	private int m_CurrentResistance;
	
	// Simulation variables
	private int m_Initiative;
	public int Initiative { get { return m_Initiative; } set { m_Initiative = value; } }
}
