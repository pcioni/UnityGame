using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	public float TransitionRate = 0.02f;
	public float sensitivity  = 5.0f;
	public float speed = 5.0f;
	public float distanceFromZoomTarget = 5.0f;  // how far from orbitTarget lerp stops

	public objectHighlightOnMouseover selected = null;
	private objectHighlightOnMouseover orbitTarget;
	private objectHighlightOnMouseover originPlanet;

	private float minFov = 60.0f;
	private float maxFov = 120.0f;

	private Vector3 lookPos;
	private Quaternion rotation;
	private Quaternion lookAtAngle;           // target slerp angle
	private Vector3 relativePos;              // relative camera position from OrbitTarget

	private Vector3 TransitionStartPos;
	private Vector3 TransitionEndPos;

	private bool canRotateCamera; // don't allow rotation during Lerp / Slerp.
	
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
		if ( selected != null && orbitTarget != selected) {
			if ( orbitTarget != null )
				orbitTarget.deselect();
			orbitTarget = selected;
			orbitTarget.select();

			Vector3 offset = ( -Camera.main.transform.forward * distanceFromZoomTarget + orbitTarget.offsetFromCenter ) * orbitTarget.offsetScaling;
			TransitionStartPos = Camera.main.transform.position;
			TransitionEndPos = orbitTarget.transform.position + offset;

			print ( TransitionEndPos );

			StartCoroutine( smoothDampToPlanet() );
		}
	}    

	// H-A-T-E, this is what stress does to me!
	float clampAngle(float angle, float min, float max) {
		
		if (angle < 90 || angle > 270){     // if angle in the critical region
			if (angle > 180) angle -= 360;  //   convert all angles to -180..+180
			if (max > 180) max -= 360;
			if (min > 180) min -= 360;
		}    
		angle = Mathf.Clamp(angle, min, max);
		if (angle < 0) angle += 360;        // if angle negative, convert to 0..360
		return angle;
	}

	//Orbit around orbitTarget's X-axis. Allows full 360 degree movement.
	void orbitCamera() {
		if (Input.GetMouseButton(1) && canRotateCamera) {
			transform.RotateAround( orbitTarget.transform.position, orbitTarget.transform.up, Input.GetAxis("Mouse X") * speed);

			Vector3 cross = Vector3.Cross(transform.forward, transform.up);
			transform.RotateAround( orbitTarget.transform.position, cross, Input.GetAxis("Mouse Y") * speed);
		}
	}
	
	// Basically zoomInToTarget() on originPlanet
	void resetCameraToOrigin() {
		Vector3 DeltaPosition = Camera.main.transform.position - orbitTarget.transform.position;

		orbitTarget.deselect();
		orbitTarget = originPlanet;

		Vector3 offset = ( -Camera.main.transform.forward * distanceFromZoomTarget + orbitTarget.offsetFromCenter ) * orbitTarget.offsetScaling;
		TransitionStartPos = Camera.main.transform.position;
		TransitionEndPos = orbitTarget.transform.position + offset;

		StartCoroutine( smoothDampToPlanet() );
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
		for (float f = 0.0f; f <= 1f; f += TransitionRate) {
			transform.position = Vector3.Lerp(TransitionStartPos, TransitionEndPos, f);
			yield return new WaitForEndOfFrame();
		}
		canRotateCamera = true;
	}
	
	void Start() {
		GameObject tmp = GameObject.Find("startingPlanet");
		orbitTarget = tmp.GetComponent<objectHighlightOnMouseover>();
		originPlanet = tmp.GetComponent<objectHighlightOnMouseover>();
		canRotateCamera = true;
	}
	
	void Update () {
		zoomInOut();
		if (Input.GetMouseButtonDown(0)) {
			zoomInOnTarget();
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			resetCameraToOrigin();
		}
		orbitCamera ();
	}
	
	
}
		