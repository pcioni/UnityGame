using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Treee : MonoBehaviour {
	//GameObject tree;
	float time_passed;
	float total_time;
	public float x_scale;
	public float y_scale;
	public float z_scale;
	float sink;
	public float c;
	public objectHighlightOnMouseover child;
	public float TimeToGrow = 2f;
	public int Ornaments = 0;
	//bool clicked;

	//public Button start;
	//public float b;

	void Start () {
		//tree = GameObject.Find ("tree");
		time_passed = 0f;
		sink = 0f;
		total_time = 0f;
	
		/*
		start.onClick.AddListener (delegate () {
			this.ButtonClicked ();
		});

		clicked = false;
		*/
	}
	
	// Update is called once per frame
	void Update () {
		if (total_time < 20) {
			float rate = Mathf.Max( 1f, Mathf.Log( (float)(Ornaments*Ornaments) ) );
			if (time_passed > TimeToGrow / rate ) {

				gameObject.transform.localScale += new Vector3 (x_scale * rate, y_scale * rate, z_scale * rate);
				time_passed = 0f;
				
				if (gameObject.transform.localScale.y < (x_scale * c +1f)){
					sink = gameObject.transform.localScale.y*3;
				}else{
					//sink = b;
				}
				
				//tree.gameObject.transform.position -= new Vector3(0f,sink,0f);
				
			} else {
				time_passed += Time.deltaTime;
			}
			
		}


	}
	/*
	void ButtonClicked() {
		if (!clicked) {
			clicked = true;
			Time.timeScale = 0.0f;

		} else {
			clicked = false;
			Time.timeScale = 1.0f;
		}
	}
*/

}
