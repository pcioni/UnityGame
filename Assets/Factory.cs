using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {
	GameObject fact1;
	GameObject fact2;
	public GUIText info;
	// Use this for initialization
	void Start () {
		fact1 = GameObject.Find ("Cube");
		fact2 = GameObject.Find ("Cube1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
