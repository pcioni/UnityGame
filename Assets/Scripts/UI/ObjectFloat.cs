using UnityEngine;
using System.Collections;

public class ObjectFloat : MonoBehaviour {

	public double rate = 1.0;
	public float amplitude = 1.0f;
	public float yoffset = 0.0f;
	private Vector3 original;

	// Use this for initialization
	void Start () {
		original = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float adjust = original.y + amplitude * Mathf.Sin((float)rate*Time.time) + yoffset;
		transform.position = new Vector3(original.x,adjust,original.z);
	}
}
