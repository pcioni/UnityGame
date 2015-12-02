using UnityEngine;

public class cameraMoveOnClick : MonoBehaviour {
	Vector3 groundCamOffset;
	Vector3 camTarget;
	Vector3 camSmoothDampV;
	
	private Vector3 GetWorldPosAtViewportPoint(float vx, float vy) {
		Ray worldRay = GetComponent<Camera>().ViewportPointToRay(new Vector3(vx, vy, 0));
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float distanceToGround;
		groundPlane.Raycast(worldRay, out distanceToGround);
		Debug.Log("distance to ground:" + distanceToGround);
		return worldRay.GetPoint(distanceToGround);
	}
	
	void Start() {
		Vector3 groundPos = GetWorldPosAtViewportPoint(0.5f, 0.5f);
		Debug.Log("groundPos: " + groundPos);
		groundCamOffset = GetComponent<Camera>().transform.position - groundPos;
		camTarget = GetComponent<Camera>().transform.position;
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			// Center whatever position is clicked
			float mouseX = Input.mousePosition.x / GetComponent<Camera>().pixelWidth;
			float mouseY = Input.mousePosition.y / GetComponent<Camera>().pixelHeight;
			Vector3 clickPt = GetWorldPosAtViewportPoint(mouseX, mouseY);
			camTarget = clickPt + groundCamOffset;
		}
		
		// Move the camera smoothly to the target position
		GetComponent<Camera>().transform.position = Vector3.SmoothDamp(
			GetComponent<Camera>().transform.position, camTarget, ref camSmoothDampV, 0.5f);
	}
}
