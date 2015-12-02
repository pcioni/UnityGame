﻿using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	
	private float minFov = 60.0f;
	private float maxFov = 120.0f;
	public float sensitivity  = 0.0f;
	
	public float speed;
	private GameObject orbitTarget;
	
	private GameObject originPlanet;
	
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
			orbitTarget = hitObject;
			Camera.main.transform.position = hitObject.transform.position;
			Camera.main.transform.rotation = hitObject.transform.rotation;
			Camera.main.transform.Translate(2, 0, 0);
		}
		
	}
	
	//Orbit around orbitTarget's X-axis
	void orbitCamera() {
		if (Input.GetMouseButton(1)) {
			transform.RotateAround(
				orbitTarget.transform.position,
				orbitTarget.transform.up,
				Input.GetAxis("Mouse X") * speed);
		}
		transform.LookAt(orbitTarget.transform, orbitTarget.transform.up);
	}

	// Basically zoomInToTarget() on originPlanet
	void resetCameraToOrigin() {
		orbitTarget = originPlanet;
		Camera.main.transform.position = originPlanet.transform.position;
		Camera.main.transform.rotation = originPlanet.transform.rotation;
		Camera.main.transform.Translate(15, 0, 0);
	}
	
	void Start() {
		orbitTarget = GameObject.Find ("startingPlanet");
		originPlanet = GameObject.Find ("startingPlanet");
		resetCameraToOrigin();
	}
	
	void Update (){
		
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
