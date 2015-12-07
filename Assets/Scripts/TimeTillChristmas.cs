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
	
	private Text timer;

	// Use this for initialization
	void Start () {
		timer = GetComponent<Text>();
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
		timer.text = string.Format("{0:00}:{1:00} " + monthNames[StartMonth] + ", " + (StartDay+1).ToString(),
		                           hrs,
		                           min);
	}
}
