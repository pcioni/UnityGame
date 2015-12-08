using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	
	public float sensitivity  = 0.0f;
	public float speed = 0.0f;
	public float distanceFromZoomTarget = 0;  // how far from orbitTarget lerp stops

	private float minFov = 60.0f;
	private float maxFov = 120.0f;

	private GameObject orbitTarget;
	private GameObject originPlanet;

	private Vector3 lookPos;
	private Quaternion rotation;
	private Quaternion lookAtAngle;           // target slerp angle
	private Vector3 relativePos;              // relative camera position from OrbitTarget

	private float startTime;                  // time our lerp begins
	private float journeyLength;			  // distance between both lerp objects
	private Transform startMarker;            
	private Transform endMarker;
	private Vector3 lerpVector;               // point on a line to lerp to.

	private bool canRotateCamera; // don't allow rotation during Lerp / Slerp.
	
	Vector3 camSmoothDampV; 
	
	
	/*
	 * Lock the camera FOV to the scrollwheel. 
	 * Change the FOV instead of distance to avoid clipping through objects.
	 */
	void zoomInOut() {
		float fov = Camera.main.fieldOfView;
		fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;
	}
	
	/*
	 * Shoot a raycast from our camera to our mouse location. 
	 *   If we hit an object, move our camera into it and offset it outside the object.
	 */
	void zoomInOnTarget() {
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit;
		
		if( Physics.Raycast( ray, out hit, 100 ) ) {
			GameObject hitObject = hit.transform.gameObject;
			if (orbitTarget != hitObject) {
				orbitTarget.GetComponent<objectHighlightOnMouseover>().deselect();
				orbitTarget = hitObject;
				orbitTarget.GetComponent<objectHighlightOnMouseover>().select();

				//Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, hitObject.transform.position, );
				//Camera.main.transform.position = hitObject.transform.position;
				Camera.main.transform.rotation = hitObject.transform.rotation;
				Camera.main.transform.Translate(2, 0, 0);

				lookPos = orbitTarget.transform.position - Camera.main.transform.position;
				rotation = Quaternion.LookRotation(lookPos);

				//keeps track of our lerp distance
				endMarker = orbitTarget.transform;
				startMarker = transform;
				startTime = Time.time;
				journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

				//slerp smoothly
				relativePos = orbitTarget.transform.position - transform.position;
				lookAtAngle = Quaternion.LookRotation(relativePos);

				//Get a normalized vector projected towards orbitTarget
				lerpVector = endMarker.position - startMarker.position;
				lerpVector = lerpVector.normalized;

				StartCoroutine("smoothDampToPlanet");
			}
		}
	}    
	
	//Orbit around orbitTarget's X-axis
	void orbitCamera() {
		if (Input.GetMouseButton(1) && canRotateCamera) {
			transform.RotateAround(
				orbitTarget.transform.position,
				orbitTarget.transform.up,
				Input.GetAxis("Mouse X") * speed);
		}
	}
	
	// Basically zoomInToTarget() on originPlanet
	void resetCameraToOrigin() {
		orbitTarget.GetComponent<objectHighlightOnMouseover>().deselect();
		orbitTarget = originPlanet;

		endMarker = orbitTarget.transform;
		startMarker = transform;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

		StartCoroutine("smoothDampToPlanet");
	}

	/*
	 * Slerp from the camera's current rotation to lookAtAngle, a Quaternion
	 *   made in zoomInOnTarget.
	 * 
	 * For lerp, we draw a line from the main camera to orbitTarget using a 
	 *   normalized vector made in zoomInOnTarget. We move along this line 
	 *   up until we're distanceFromZoomTarget from orbitTarget's origin. 
	 *   Our lerp destination matches the camera's 'y' position to orbitTarget's.
	 */ 
	IEnumerator smoothDampToPlanet() {
		canRotateCamera = false;
		for (float f = 0.0f; f <= 1f; f += .02f) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			Vector3 lerpTo = (endMarker.position + (distanceFromZoomTarget * lerpVector));
			Vector3 matchHeight = new Vector3(lerpTo.x, orbitTarget.transform.position.y, lerpTo.z);
			lerpTo = matchHeight;
			
			transform.position = Vector3.Lerp(startMarker.position, lerpTo, fracJourney);
			transform.rotation = Quaternion.Slerp(startMarker.rotation, lookAtAngle, Time.deltaTime * speed); // 2 = damping
			yield return new WaitForEndOfFrame();
		}
		canRotateCamera = true;
	}
	
	void Start() {
		orbitTarget = GameObject.Find ("startingPlanet");
		originPlanet = GameObject.Find ("startingPlanet");
		canRotateCamera = true;
	}
	
	void Update () {
		
		zoomInOut();
		
		if (Input.GetMouseButtonDown(0)) {
			zoomInOnTarget ();
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			resetCameraToOrigin();
		}


		orbitCamera ();
	
		
	}
	
	
}
		