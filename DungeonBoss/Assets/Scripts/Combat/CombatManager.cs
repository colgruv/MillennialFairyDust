using UnityEngine;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour 
{
	private List<CombatCharacterController> m_CombatCharacters;

	public int PrecalculatedTurnOrderLength;
	private List<CombatCharacterController> m_PrecalculatedTurnOrder;
	public List<CombatCharacterController> PrecalculatedTurnOrder { get { return m_PrecalculatedTurnOrder; } }

	private int[] savedInitiatives;
	private int m_baseSpeed = 0;
	private int m_numTurns = 0;
	public int StageEffectInterval = 5;

	private GridManager m_GridManager;

	public CombatCharacterController[] ManualCharacterArray;

	void Awake()
	{
		//Debug.Log("CombatManager::Start()");

		m_CombatCharacters = new List<CombatCharacterController>();
		for (int i = 0; i < ManualCharacterArray.Length; i++)
		{
			m_CombatCharacters.Add(ManualCharacterArray[i]);
		}

		CalculateTurnOrder();

		m_GridManager = gameObject.GetComponent<GridManager>();
	}

	void Start()
	{
		placeCharactersRandomly();
		calculateBaseSpeed();
		placeEffectsRandomly();
	}

	void OnEnable()
	{
		//CombatEvent.EventComplete += CalculateTurnOrder;
	}
	
	void OnDisable()
	{
		//CombatEvent.EventComplete -= CalculateTurnOrder;
	}

	void CalculateTurnOrder()
	{
		//Debug.Log("CalculateTurnOrder()");

		// Clear the turn order
		m_PrecalculatedTurnOrder = new List<CombatCharacterController>();

		for (int iter_turnOrderLength = 0; iter_turnOrderLength < PrecalculatedTurnOrderLength; iter_turnOrderLength++)
		{
			// Find the character with the highest Initiative, keeping track of its index.
			int highestInitiative = 0; 
			int highestInitiativeIndex = 0;
			for (int iter_numCombatChars = 0; iter_numCombatChars < m_CombatCharacters.Count; iter_numCombatChars++)
			{
				if (m_CombatCharacters[iter_numCombatChars].Initiative > highestInitiative)
				{
					highestInitiativeIndex = iter_numCombatChars;
					highestInitiative = m_CombatCharacters[iter_numCombatChars].Initiative;
				}
			}

			// Add the character with the highest Initiative to the Turn Order
			m_PrecalculatedTurnOrder.Add(m_CombatCharacters[highestInitiativeIndex]);

			// Add each character's Speed to their Initiative, keeping track of which are over 1000.
			// Exclude the character that was just added to the Turn Order.
			bool anyUnder1000 = false;
			for (int iter_numCombatChars = 0; iter_numCombatChars < m_CombatCharacters.Count; iter_numCombatChars++)
			{
				if (iter_numCombatChars == highestInitiativeIndex)
					m_CombatCharacters[iter_numCombatChars].Initiative -= m_baseSpeed;

				m_CombatCharacters[iter_numCombatChars].Initiative += m_CombatCharacters[iter_numCombatChars].Speed;

				if (m_CombatCharacters[iter_numCombatChars].Initiative < 1000)
					anyUnder1000 = true;
			}

			// If all characters' initiatives are over 1000, clamp them (to keep int values in check)
			if (!anyUnder1000)
			{
				Debug.Log("Clamping all Characters' Initiative to < 1000");
				for (int iter_numCombatChars = 0; iter_numCombatChars < m_CombatCharacters.Count; iter_numCombatChars++)
				{
					m_CombatCharacters[iter_numCombatChars].Initiative -= 1000;

					// TODO: Make a stage event happen.
				}
			}

			// Store the values after one iteration. This is the "real" value that determines the next character turn.
			if (iter_turnOrderLength == 0)
				saveCharacterInitiatives();
		}

		// After the entire turn order has been simulated, revert all character Initiatives to the saved value.
		loadCharacterInitiatives();
	}

	public CharacterTurnEvent GetNextCharacterTurn()
	{


		if (m_PrecalculatedTurnOrder[0].CharacterControlType == CombatCharacterController.ControlType.NPC)
		{
			EnemyTurnEvent beginCharacterTurnEvent = new EnemyTurnEvent();
			beginCharacterTurnEvent.Character = m_PrecalculatedTurnOrder[0];
			m_PrecalculatedTurnOrder[0].TurnsGiven++;
			CalculateTurnOrder();
			return beginCharacterTurnEvent;
		}
		else if (m_PrecalculatedTurnOrder[0].CharacterControlType == CombatCharacterController.ControlType.PC)
		{
			Debug.Log("Player turn.");

			PlayerTurnEvent beginCharacterTurnEvent = new PlayerTurnEvent();
			beginCharacterTurnEvent.Character = m_PrecalculatedTurnOrder[0];
			m_PrecalculatedTurnOrder[0].TurnsGiven++;
			CalculateTurnOrder();
			return beginCharacterTurnEvent;
		}
		else
			return null;
	}

	private void saveCharacterInitiatives()
	{
		savedInitiatives = new int[m_CombatCharacters.Count];
		for (int i = 0; i < m_CombatCharacters.Count; i++)
		{
			savedInitiatives[i] = m_CombatCharacters[i].Initiative;
		}
	}

	private void loadCharacterInitiatives()
	{
		for (int i = 0; i < m_CombatCharacters.Count; i++)
		{
			m_CombatCharacters[i].Initiative = savedInitiatives[i];
		}
	}

	private void placeCharactersRandomly()
	{
		for (int i = 0; i < m_CombatCharacters.Count; i++)
		{
			if (m_CombatCharacters[i].CharacterControlType == CombatCharacterController.ControlType.PC)
			{
				m_CombatCharacters[i].transform.SetParent(m_GridManager.GridHexes[0].transform);
				m_CombatCharacters[i].transform.localPosition = Vector3.zero;
				m_CombatCharacters[i].transform.localRotation = Quaternion.identity;
			}
			else
			{
				int placeIndex = Random.Range(1, m_GridManager.GridHexes.Count - 1);
				m_CombatCharacters[i].transform.SetParent(m_GridManager.GridHexes[placeIndex].transform);
				m_CombatCharacters[i].transform.localPosition = Vector3.zero;
				m_CombatCharacters[i].transform.localRotation = Quaternion.identity;
			}
		}
	}

	/// <summary>
	/// Places the effects randomly.
	/// </summary>
	private void placeEffectsRandomly()
	{
		for (int i = 0; i < m_GridManager.GridHexes.Count; i++)
		{
			if (i % 10 == 0)
			{
				m_GridManager.GridHexes[i].AddEffect(GridHex.Effect.FIRE);
			}
		}
	}

	private void calculateBaseSpeed()
	{
		m_baseSpeed = 0;
		for (int i = 0; i < m_CombatCharacters.Count; i++)
		{
			m_baseSpeed += m_CombatCharacters[i].Speed;
		}
	}

	private void placeNewEffect()
	{

	}
}
