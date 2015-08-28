using UnityEngine;
using System.Collections;

public class ActionButtonsTransitions : MonoBehaviour {

	public Animator transitions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleHidden()
	{
		transitions.SetBool("hidden", !transitions.GetBool("hidden"));
	}

	public void ToggleSpells()
	{
		transitions.SetBool("spells", !transitions.GetBool("spells"));
	}
}
