using UnityEngine;
using System.Collections;

public class workerController : MonoBehaviour {

	private bool isSmacked = false;

	public void getWhipped() {
		//turn him red and make him fall over
		GetComponent<Renderer>().material.color = Color.red;
		transform.Rotate(0, 90, 0);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
