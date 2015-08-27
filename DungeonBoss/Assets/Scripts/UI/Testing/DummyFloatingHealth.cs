using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DummyFloatingHealth : MonoBehaviour {

	public Slider floatingHealth;
	public float totalHealth;
	public float currentHealth;
	private float percentHealth;

	// Use this for initialization
	void Start () {
		percentHealth = currentHealth/totalHealth;
		floatingHealth.value = percentHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{

		currentHealth += Random.Range(-15,0);
		if (currentHealth <= 0f)
		{
			Destroy(transform.parent.gameObject);
		}

		percentHealth = currentHealth/totalHealth;
		floatingHealth.value = percentHealth;

	}
}
