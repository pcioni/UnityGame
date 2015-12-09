using UnityEngine;
using System.Collections;
using System.Linq;

public class cameraController : MonoBehaviour {
	
	public float sensitivity  = 0.0f;
	public float speed = 0.0f;
	public float distToCollide = 0.0f;

	public objectHighlightOnMouseover selected = null;
	private objectHighlightOnMouseover orbitTarget = null;
	private objectHighlightOnMouseover originPlanet = null;
	
	private Quaternion lookAtAngle;           // target slerp angle
	private Vector3 relativePos;              // relative camera position from OrbitTarget
	
	private Transform startMarker;            
	private Transform endMarker;
	private Vector3 lerpVector;               // point on a line to lerp to.
	
	private bool canRotateCamera;             // don't allow rotation during Lerp / Slerp.
	
	private Vector3 camSmoothDampV;

	/*
	 * Lock the camera FOV to the scrollwheel. 
	 * Change the FOV instead of distance to avoid clipping through objects.
	 */
	void zoomInOut() {
		float dist = Vector3.Distance(transform.position, orbitTarget.transform.position);
		float i = Input.GetAxis("Mouse ScrollWheel");
		if ( Input.GetKey(KeyCode.A) ) {
			i = 0.01f;
		} else if ( Input.GetKey(KeyCode.Q) ) {
			i = -0.01f;
		}

		if ( Input.GetKey(KeyCode.A) || ( dist > distToCollide && i > 0 ) ) {
			Vector3 moveDir = -(i * sensitivity) * transform.TransformDirection(new Vector3( 0, 0, -dist / 10)); 
			transform.position += moveDir * Time.deltaTime; 
		}
		else if ( Input.GetKey(KeyCode.Q) || i < 0 ) {
			Vector3 moveDir = -(i * sensitivity) * transform.TransformDirection(new Vector3( 0, 0, -dist / 10)); 
			transform.position += moveDir * Time.deltaTime; 
		}

	}

	void checkAzimuthAgainstOrigin() {
		var newModule = transform;
		var forwardVectorToMatch = orbitTarget.transform.forward;
		var correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(orbitTarget.transform.forward);
		newModule.RotateAround(orbitTarget.transform.position, Vector3.up, correctiveRotation);
		var correctiveTranslation = transform.position - orbitTarget.transform.position;
		transform.position += correctiveTranslation;
	}

	private static float Azimuth(Vector3 vector) {
		return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
	}

	// initialize our lerp / slerp values and call the coroutine
	void LerpToTarget() {
		//keeps track of our lerp distance
		endMarker = orbitTarget.transform;
		startMarker = transform;
		
		//slerp smoothly
		relativePos = orbitTarget.transform.position - transform.position;
		lookAtAngle = Quaternion.LookRotation(relativePos);
		
		//Get a normalized vector projected towards orbitTarget
		lerpVector = endMarker.position - startMarker.position;
		lerpVector = lerpVector.normalized;
		
		StartCoroutine("smoothDampToPlanet");
	}
	
	/*
	 * Shoot a raycast from our camera to our mouse location. 
	 *   If we hit an object, move our camera into it and offset it outside the object.
	 */
	void zoomInOnTarget() {
		if ( selected != null && selected.IsActive && orbitTarget != selected) {
			orbitTarget.deselect();
			orbitTarget = selected;
			orbitTarget.select();

			LerpToTarget();
		}
	}    
	
	// Clamp our angles because apparently Unity doesn't know how.
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
		orbitTarget.deselect();
		orbitTarget = originPlanet;

		endMarker = orbitTarget.transform;
		startMarker = transform;

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
		for (float f = 0.0f; f <= 1f; f += .02f * Time.timeScale) {
			float ScaledDist = -orbitTarget.offsetScaling;
			Vector3 lerpTo = (endMarker.position + (ScaledDist * lerpVector));
			
			transform.position = Vector3.Lerp(startMarker.position, lerpTo, f);
			transform.rotation = Quaternion.Slerp(startMarker.rotation, lookAtAngle, Time.deltaTime * speed); // 2 = damping
			yield return new WaitForEndOfFrame();
		}
		canRotateCamera = true;
	}
	
	void Start() {
		objectHighlightOnMouseover tmp = GameObject.Find("startingPlanet").GetComponent<objectHighlightOnMouseover>();
		orbitTarget = tmp;
		originPlanet = tmp;
		canRotateCamera = true;
		LerpToTarget();
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
