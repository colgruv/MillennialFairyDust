using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DummyHealthObject : MonoBehaviour {

	public Slider visualizer;
	public Slider healthBar;

	public float currentHealth;
	public float totalHealth;
	private float percentHealth;

	public Text healthDisplay;
	public Text healthTotalDisplay;

	public Text minusText;
	public Text plusText;

	// Use this for initialization
	void Start () {

		healthTotalDisplay.text = totalHealth.ToString();
		healthDisplay.text = currentHealth.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void DisplayText()
	{
		healthDisplay.text = currentHealth.ToString();
	}
	
	private void UpdateHealth(float delta)
	{
		currentHealth = Mathf.Clamp(currentHealth + delta,0,totalHealth);
		DisplayText();
		percentHealth = currentHealth/totalHealth;
		visualizer.value = percentHealth;
		healthBar.value = percentHealth;
	}

	public void IncreaseHealth(float delta)
	{
		float randomized =  (int)Random.Range(delta * -1, delta) + delta;
		UpdateHealth(randomized);
		plusText.text = randomized.ToString();
	}

	public void DecreaseHealth(float delta)
	{
		float randomized =  (int)Random.Range(delta * -1, delta) + delta;
		UpdateHealth(randomized);
		minusText.text = randomized.ToString();
	}

}
