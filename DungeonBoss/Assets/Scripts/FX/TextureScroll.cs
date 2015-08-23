using UnityEngine;
using System.Collections;

public class TextureScroll : MonoBehaviour {

	public float x,y;

	Renderer r;

	// Use this for initialization
	void Start () {
		r = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		float offsetX = Time.time * x;
		float offsetY = Time.time * y;
		r.material.SetTextureOffset("_MainTex", new Vector2(offsetX,offsetY));
	}
}
