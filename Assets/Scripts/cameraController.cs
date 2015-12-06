using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	
	private float minFov = 60.0f;
	private float maxFov = 120.0f;
	public float sensitivity  = 0.0f;
	
	public float speed;
	private GameObject orbitTarget;
	
	private GameObject originPlanet;
	
	private Vector3 lookPos;
	private Quaternion rotation;

	private float startTime;
	private float journeyLength;
	private Transform startMarker;
	private Transform endMarker;
	private Quaternion lookAtAngle;
	private Vector3 relativePos;
	private Vector3 lerpVector;
	public float distanceFromZoomTarget = 0;

	private bool canRotateCamera;
	
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
				lookPos = orbitTarget.transform.position - Camera.main.transform.position;
				rotation = Quaternion.LookRotation(lookPos);

				endMarker = orbitTarget.transform;
				startMarker = transform;
				startTime = Time.time;
				journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

				relativePos = orbitTarget.transform.position - transform.position;
				lookAtAngle = Quaternion.LookRotation(relativePos);

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
		//transform.LookAt(orbitTarget.transform, orbitTarget.transform.up);
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

	// Lerp from the camera position to slightly in front of the orbitTarget
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

		transform.rotation = new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w);

		orbitCamera ();
	
		
	}
	
	
}
		