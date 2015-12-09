using UnityEngine;
using System.Collections;

public class whipSmack : MonoBehaviour {

	private Vector3 delta = Vector3.zero;
	private Vector3 lastPos = Vector3.zero;

	void getMouseVelocity() {
		if ( Input.GetMouseButtonDown(0) ) {
			lastPos = Input.mousePosition;
		}
		else if ( Input.GetMouseButton(0) ) {
			delta = Input.mousePosition - lastPos;
			//Debug.Log( "delta X : " + delta.x );
			//Debug.Log( "delta Y : " + delta.y );
			//Debug.Log( "delta distance : " + delta.magnitude );
			lastPos = Input.mousePosition;
		}	
	}

	void Update () {
		getMouseVelocity();
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.name == "worker") {
                    hit.transform.gameObject.GetComponent<workerController>().getWhipped();
                }
            }
        }

	}
}
