using UnityEngine;
using System.Collections;

public class objectHighlightOnMouseover : MonoBehaviour {

	private Color startcolor;
	void OnMouseEnter() {
		startcolor = GetComponent<Renderer>().material.color;
		GetComponent<Renderer>().material.color = Color.yellow;
	}
	void OnMouseExit() {
		GetComponent<Renderer>().material.color = startcolor;
	}
}
