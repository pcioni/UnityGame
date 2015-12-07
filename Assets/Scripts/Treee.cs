using UnityEngine;
using System.Collections;

public class Treee : MonoBehaviour {
	GameObject tree;
	float time_passed;
	float total_time;
	public float x_scale;
	public float y_scale;
	public float z_scale;
	float sink;
	public float c;
	//public float b;
	// Use this for initialization
	void Start () {
		tree = GameObject.Find ("tree");
		time_passed = 0f;
		sink = 0f;
		total_time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (total_time < 20) {
			if (time_passed > 2) {
				
				tree.gameObject.transform.localScale += new Vector3 (x_scale, y_scale, z_scale);
				time_passed = 0f;
				
				if (tree.gameObject.transform.localScale.y < (x_scale * c +1f)){
					sink = tree.gameObject.transform.localScale.y*3;
				}else{
					//sink = b;
				}
				
				//tree.gameObject.transform.position -= new Vector3(0f,sink,0f);
				
			} else {
				time_passed += Time.deltaTime;
			}
			
		}
	}
}
