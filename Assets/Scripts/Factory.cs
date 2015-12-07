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
}
