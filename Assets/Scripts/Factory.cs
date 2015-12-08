using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Factory : MonoBehaviour {
	
	public Text info;
	float manufacture_time;
	public float speed;
	public GameObject orn_mesh;
	public Material m1,m2,m3,m4,m5;
	Material[] opts;

	void Start () {
		manufacture_time = 0f;
		opts = new Material[6];
		opts [0] = m1;
		opts [1] = m1;
		opts [2] = m2;
		opts [3] = m3;
		opts [4] = m4;
		opts [5] = m5;
	}

	void Update () {
		manufacture_time += Time.deltaTime;
		if (manufacture_time > 30f) {
			manufacture_time = 0f;
			GameObject ornament = (GameObject) Instantiate (orn_mesh, gameObject.transform.position + transform.forward*2.5f, transform.rotation);
			ornament.transform.forward = transform.forward;
		}
	}

	void OnMouseDown(){
		if (info.text == "Got Me") {
			info.text = "";
		} else {
			info.text = "Got Me";
		}
	}
	
}
