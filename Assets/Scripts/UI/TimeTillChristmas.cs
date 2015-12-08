using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeTillChristmas : MonoBehaviour {

	static private int[] monthLen = { 31,28,31,30,31,30,31,31,30,31,30,31 };
	static private string[] monthNames = { 
		"January",
		"February",
		"March", 
		"April", 
		"May", 
		"June", 
		"July", 
		"August", 
		"September", 
		"October", 
		"November", 
		"December"
	};

	public double GameMinutesPerWorldSeconds = 1.0;
	public int StartMonth = 0;
	public int StartDay = 0;
	public int StartHour = 0;
	public double StartMinute = 0.0;
	private int DaysPassed = 0;

	public Text messages;
	public Button accept;
	
	private Text timer;
	private int gift_counter;
	private string[] gifts;

	// Use this for initialization
	void Start () {
		timer = GetComponent<Text>();
		gift_counter = 0;
		gifts = new string[26];
		gifts [0] = "Ornaments";
		gifts [1] = "Stocking";
		gifts [2] = "Tinsel";
		gifts [3] = "Generator";
		gifts [4] = "Ornaments";
		gifts [5] = "Tinsel";
		gifts [6] = "Dated Ornament";
		gifts [7] = "String of Rainbow Lights";
		gifts [8] = "Musical Ornament";
		gifts [9] = "Tinsel";
		gifts [10] = "Ornaments";
		accept.gameObject.SetActive (false);
		messages.gameObject.SetActive (false);
		accept.onClick.AddListener (delegate () {
			this.ButtonClicked ();
		});
	}

	private void DayEvent(){
		Time.timeScale = 0.0f;

		messages.text = "Your daily present is " + gifts[DaysPassed] + "!!! (You're so lucky!)";

		accept.gameObject.SetActive(true);
		messages.gameObject.SetActive (true);

		gift_counter ++ ;
	}
	
	// Update is called once per frame
	void Update () {
		StartMinute += Time.deltaTime * GameMinutesPerWorldSeconds;
		while ( StartMinute >= 60.0 ) {
			StartHour ++ ;
			StartMinute -= 60.0;
			if ( StartHour == 24 ) {
				StartHour = 0;
				StartDay ++ ;

				DayEvent();

				DaysPassed ++ ;

				if ( StartDay == monthLen[StartMonth] ) {
					StartDay = 0;
					StartMonth ++ ;
					if ( StartMonth == 12 ) {
						StartMonth = 0;
					}
				}
			}
		}
		int hrs = ( StartHour + 11 ) % 12 + 1;
		int min = (int) StartMinute;
		string working;
		if ( StartHour >= 12 )
			working = "PM";
		else
			working = "AM";
		timer.text = string.Format("{0:00}:{1:00} " + working + " " + monthNames[StartMonth] + ", " + (StartDay+1).ToString(),
		                           hrs,
		                           min);
	}

	public void ButtonClicked() {
		Time.timeScale = 1.0f;
		accept.gameObject.SetActive(false);
		messages.gameObject.SetActive(false);
	}
}
