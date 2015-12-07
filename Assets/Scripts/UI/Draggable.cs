using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private bool justCreated = true;

	// Use this for initialization
	void Start () {
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position -
				 Camera.main.ScreenToWorldPoint(
						new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)
				 );
		justCreated = true;
	}
	
	void Update() {
		if ( justCreated ) {
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
			transform.position = curPosition;
		}
	}
	void OnMouseUp() {
		justCreated = false;
	}
}
