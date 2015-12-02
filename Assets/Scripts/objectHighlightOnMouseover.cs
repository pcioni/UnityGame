using UnityEngine;
using System.Collections;

public class objectHighlightOnMouseover : MonoBehaviour {

	private Color startcolor;
	private bool currentlySelected;

	void OnMouseEnter() {
		if (!currentlySelected) {
			startcolor = GetComponent<Renderer>().material.color;
			GetComponent<Renderer>().material.color = Color.yellow;
		}
	}
	void OnMouseExit() {
		GetComponent<Renderer>().material.color = startcolor;
	}

	public void select() {
		currentlySelected = true;
	}

	public void deselect() {
		currentlySelected = false;
	}

	void Start() {
		currentlySelected = false;
	}

}
