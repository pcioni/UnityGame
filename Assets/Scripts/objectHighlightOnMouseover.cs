using UnityEngine;
using System.Collections;

public class objectHighlightOnMouseover : MonoBehaviour {

	public bool IsActive = false;
	public Vector3 offsetFromCenter = new Vector3(0,0,0);
	public float offsetScaling = 1f;
	public UIReq[] factories;

	private Color startcolor;
	private bool currentlySelected;
	private cameraController ctrl;

	public void SetToActive() {
		IsActive = true;
		foreach( UIReq fact in factories ) {
			fact.IsActive = true;
		}
	}

	void OnMouseEnter() {
		if ( IsActive && Time.timeScale != 0.0 && !currentlySelected) {
			GetComponent<Renderer>().material.color = Color.yellow;
			ctrl.selected = this;
		}
	}
	void OnMouseExit() {
		if ( Time.timeScale != 0.0 ) {
			GetComponent<Renderer>().material.color = startcolor;
			if ( ctrl.selected == this )
				ctrl.selected = null;
		}
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
		startcolor = GetComponent<Renderer>().material.color;
		factories = GetComponentsInChildren<UIReq>();
	}

}
