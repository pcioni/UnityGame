using UnityEngine;
using System.Collections;

public class whipSmack : MonoBehaviour {

	private Vector3 delta = Vector3.zero;
	private Vector3 lastPos = Vector3.zero;
	private bool tracking = false;

	void getMouseVelocity() {
		if ( Input.GetMouseButtonDown(0) ) {
			lastPos = Input.mousePosition;
		}
		else if ( Input.GetMouseButton(0) ) {
			delta = Input.mousePosition - lastPos;
			
			// Do Stuff here
			
			Debug.Log( "delta X : " + delta.x );
			Debug.Log( "delta Y : " + delta.y );
			
			Debug.Log( "delta distance : " + delta.magnitude );
			
			// End do stuff
			
			lastPos = Input.mousePosition;
		}	
	}

	// On hit,
	void OnTriggerEnter(Collider other) {
		// check if delta distance is high enough,
		// or some other form of speed checking
		other.GetComponent<workerController>().getWhipped();
		print ("whip collision");
	}

	void Update () {
		getMouseVelocity();
		Vector3 distFromCamera = Input.mousePosition;
		distFromCamera.z = 1f; 						  //  draw the object this far from the camera
		if (Input.GetKeyDown(KeyCode.E)) {
			tracking = !tracking;
		}
		if (tracking) {								  //  follow the mouse
			this.transform.rotation = Camera.main.transform.rotation;
			this.transform.position = Camera.main.ScreenToWorldPoint(distFromCamera);
		}

	}
}
