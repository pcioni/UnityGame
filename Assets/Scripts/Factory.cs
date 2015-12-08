using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Factory : MonoBehaviour {
	
	public Text info;

	void Start () {
	
	}

	void Update () {
		
	}

	void OnMouseDown(){
		if (info.text == "Got Me") {
			info.text = "";
		} else {
			info.text = "Got Me";
		}
	}

	void OnCollisionEnter(Collision decor){
		Debug.Log ("Hit");
		gameObject.transform.SetParent(decor.gameObject.transform);
		//Destroy (decor.rigidbody);
	}
}
