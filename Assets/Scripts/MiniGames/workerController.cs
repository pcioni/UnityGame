using UnityEngine;
using System.Collections;

public class workerController : MonoBehaviour {

	private bool isSmacked = false;
	private Color startColor;
	public int chanceOfSpawning = 0;

	IEnumerator wait() {
		for (float f = 0.0f; f <= 1f; f += 1f) {
			yield return new WaitForSeconds(1);
		}
	}

	public void getWhipped() {
		//turn him red and make him fall over
		transform.Rotate(0, 90, 0);
		transform.Rotate(0, -90, 0);
		isSmacked = false;
		GetComponent<Renderer>().material.color = startColor;
	}

	// Use this for initialization
	void Start () {
		startColor = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		int rand = Random.Range(0, chanceOfSpawning);
		if (rand == 1) {
			isSmacked = true;
			GetComponent<Renderer>().material.color = Color.red;
		}
	}
}
