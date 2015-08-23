using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour {

	public float x,y,z;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(x,y,z);
	}
}
