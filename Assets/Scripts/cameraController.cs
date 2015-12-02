using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	private float minFov = 80.0f;
	private float maxFov = 120.0f;
	public float sensitivity  = 0.0f;

	public float speed;
	private GameObject orbitTarget;

	private Plane plane = new Plane(Vector3.up, Vector3.zero);
	private Vector3 v3Center = new Vector3(0.5f,0.5f,0.0f);
	

	void zoomInOut() {
		float fov = Camera.main.fieldOfView;
		fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;
	}

	void zoomInOnTarget() {
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit;
		
		if( Physics.Raycast( ray, out hit, 100 ) ) {
			GameObject hitObject = hit.transform.gameObject;
			orbitTarget = hitObject;
			transform.LookAt(hitObject.transform);
		}

	}

	void orbitCameraOnRightClick() {
		if (Input.GetMouseButton(1)) {
			transform.RotateAround(
				orbitTarget.transform.position,
				orbitTarget.transform.up,
				Input.GetAxis("Mouse X") * speed);
			transform.RotateAround(
				orbitTarget.transform.position,
				orbitTarget.transform.right,
				Input.GetAxis("Mouse Y") * speed);
		}
		transform.LookAt(orbitTarget.transform, orbitTarget.transform.up);
	}
	
	void Start() {
		orbitTarget = GameObject.Find ("startingPlanet");
	}

	void Update (){

		zoomInOut();

		if (Input.GetMouseButtonDown (0)) {
			zoomInOnTarget ();
		}

		orbitCameraOnRightClick ();
	}


}
