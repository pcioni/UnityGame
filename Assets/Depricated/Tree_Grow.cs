using UnityEngine;
using System.Collections;

public class Tree_Grow: MonoBehaviour {
	GameObject tree;
	float time_passed;
	float total_time;
	public float x_scale;
	public float y_scale;
	public float z_scale;
	public float c;
	//public float b;
	// Use this for initialization
	void Start () {
		tree = GameObject.Find ("tree");
		time_passed = 0f;
		total_time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (total_time < 20) {
			if (time_passed > 2) {
				
				tree.gameObject.transform.localScale += new Vector3 (x_scale, y_scale, z_scale);
				time_passed = 0f;
				
			} else {
				time_passed += Time.deltaTime;
			}
			
		}
	}
}


