using UnityEngine;
using System.Collections;

public class PointAtCamera : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.LookAt( Camera.main.transform.position, Vector3.up );
		transform.Rotate ( new Vector3(0,180,0) );
	}
}
