using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game_Time : MonoBehaviour {
	float day_time;
	int day,month,year,gift_counter;
	public Text messages;
	public Button accept;
	string[] gifts;

	// Use this for initialization
	void Start() {
		day_time = 118f;
		day = 1;
		month = 12;
		year = 2015;
		gift_counter = 0;
		gifts = new string[26];
		gifts [0] = "";
		gifts [1] = "Ornaments";
		gifts [2] = "Stocking";
		gifts [3] = "Tinsel";
		gifts [4] = "Generator";
		gifts [5] = "Ornaments";
		gifts [6] = "Tinsel";
		gifts [7] = "Dated Ornament";
		gifts [8] = "String of Rainbow Lights";
		gifts [9] = "Musical Ornament";
		gifts [10] = "Tinsel";
		gifts [11] = "Ornaments";
		accept.gameObject.SetActive (false);
		messages.gameObject.SetActive (false);
		accept.onClick.AddListener (delegate () {
			this.ButtonClicked ();
		});

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (day_time);
		if (day_time > 120f) {
			day_time = 0f;
			day += 1;
			Time.timeScale = 0.0f;
			accept.gameObject.SetActive(true);
			messages.text="Your daily present is: " + gifts[day];
			messages.gameObject.SetActive (true);
		} else {
			day_time += Time.deltaTime;
		}
	}

	public void ButtonClicked() {
		Time.timeScale = 1.0f;
		accept.gameObject.SetActive(false);
		messages.gameObject.SetActive(false);

	}
}
