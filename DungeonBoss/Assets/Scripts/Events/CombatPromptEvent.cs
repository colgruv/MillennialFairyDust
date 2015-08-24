using UnityEngine;
using System.Collections;

public class CombatPromptEvent : CharacterControllerEvent 
{
	protected UIManager UIManager
	{
		get { return GameObject.FindGameObjectWithTag("UIRoot").GetComponent<UIManager>(); }
	}

	protected RectTransform GetPanel(string _name)
	{
		RectTransform[] panels = UIManager.UIPanels;

		//GameObject[] UIPanels = GameObject.FindGameObjectsWithTag("UIPanel");
		for (int i = 0; i < panels.Length; i++)
		{
			if (panels[i].name == _name)
				return panels[i];
		}

		Debug.LogWarning("Panel of the given name not found.");
		return null;
	}

	public override void Init()
	{

	}

	public override void Process()
	{

	}
}
