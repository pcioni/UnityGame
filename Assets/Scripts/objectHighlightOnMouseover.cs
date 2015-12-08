using UnityEngine;
using System.Collections;

public class objectHighlightOnMouseover : MonoBehaviour {

	public Vector3 offsetFromCenter = new Vector3(0,0,0);
	private Color startcolor;
	private bool currentlySelected;
	private cameraController ctrl;

	void OnMouseEnter() {
		if ( Time.timeScale != 0.0 && !currentlySelected) {
			startcolor = GetComponent<Renderer>().material.color;
			GetComponent<Renderer>().material.color = Color.yellow;
			ctrl.selected = this;
		}
	}
	void OnMouseExit() {
		GetComponent<Renderer>().material.color = startcolor;
		if ( ctrl.selected == this )
			ctrl.selected = null;
	}

	public void select() {
		currentlySelected = true;
	}

	public void deselect() {
		currentlySelected = false;
	}

	void Start() {
		currentlySelected = false;
		ctrl = Camera.main.GetComponent<cameraController>();
	}

}
