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
	private int DayIndex = 0;

	[TextArea(3,10)]
	public string[][] TextPerDay = new string[][] {
		/* 01 */ new string[] {	"As the Sun rises over Christmas Town, a small population comes to life. The children of the village look outside - there's snow on the ground! It's Christmas Season!",
								"In the center of Town, the villagers plant a small tree, and begin their yearly tradition. Each year, the villagers of Christmas Town decorate their tree with ornaments.",
								"As more decorations go on the tree, the tree will grow, spreading Christmas cheer across the world!",
								"This year, it's your turn to be in charge of the tree's growth. Christmas Town is depending on you, so spread Christmas cheer far and wide!" }, 
		/* 02 */ new string[] {	"One day has passed, and your tree is already showing signs of growth!" },
		/* 03 */ new string[] { },
		/* 04 */ new string[] { "As the villagers of Christmas Town wake up for another busy day, they are delighted to see that the tree is now taller than you!" },
		/* 05 */ new string[] { },
		/* 06 */ new string[] { },
		/* 07 */ new string[] { "One week has passed. The tree has grown so tall that we can make ornaments on the moon!" },
		/* 08 */ new string[] {},
		/* 09 */ new string[] {},
		/* 10 */ new string[] { "Only two weeks until Christmas Eve! Everyone is jumping with joy (at the time off)! Ornaments are now being produced on Mars!" },
		/* 11 */ new string[] { "A beautiful Saturday morning. Snow is falling, children are sledding, and the tree has overtaken half of the country!" },
		/* 12 */ new string[] {},
		/* 13 */ new string[] { "Having overtaken the world, the tree begins to grow upwards!" },
		/* 14 */ new string[] {},
		/* 15 */ new string[] {},
		/* 16 */ new string[] {},
		/* 17 */ new string[] {},
		/* 18 */ new string[] { "Our tree is the size of Jupiter! We can also produce ornaments on Jupiter!" },
		/* 19 */ new string[] {},
		/* 20 */ new string[] {},
		/* 21 */ new string[] {},
		/* 22 */ new string[] {},
		/* 23 */ new string[] {},
		/* 24 */ new string[] { "Only one more day to go. Our tree has grown to be the size of the solar system, reaching all the way out to tiny Pluto!" },
		/* 25 */ new string[] { "Merry Christmas everyone! We all worked so hard to make this years Christmas tree better than ever before. Let us all take a moment to appreciate this wonderful holiday...",
								"Now back to work everyone. We have to get ready for next year." }
	};
	public float[] RequiredSize = new float[30];
	
	public Text messages;
	public Button accept;
	public GameObject panel;
	
	private Text timer;

	public GameObject tree;
	public Image win;
	public Image fail;

	public float limit;

	// Use this for initialization
	void Start () {
		timer = GetComponent<Text>();
		accept.gameObject.SetActive (false);
		messages.gameObject.SetActive (false);
		panel.SetActive(false);
		win.gameObject.SetActive( false );
		fail.gameObject.SetActive( false );
		accept.onClick.AddListener (delegate () {
			this.ButtonClicked ();
		});
	}
	private void TriggerEvent() {
		print ( "Event Trigger " + DaysPassed.ToString() );
		if ( DaysPassed == 7 ) {
			print ( "Moon is now available" );
			objectHighlightOnMouseover moon = GameObject.Find("Moon").GetComponent<objectHighlightOnMouseover>();
			moon.SetToActive();
		}
		if ( DaysPassed == 10 ) {
			print ( "Mars is now available" );
			objectHighlightOnMouseover moon = GameObject.Find("Mars").GetComponent<objectHighlightOnMouseover>();
			moon.SetToActive();
		}
		if ( DaysPassed == 18 ) {
			print ( "Jupiter is now available" );
			objectHighlightOnMouseover moon = GameObject.Find("Jupiter").GetComponent<objectHighlightOnMouseover>();
			moon.SetToActive();
		}
		if ( DaysPassed == 24 ) {
			print ( "Pluto is now available" );
			objectHighlightOnMouseover moon = GameObject.Find("Pluto").GetComponent<objectHighlightOnMouseover>();
			moon.SetToActive();
		}
	}
	private void DayEvent(){
		if ( TextPerDay.Length <= DaysPassed || TextPerDay[DaysPassed].Length <= DayIndex ) {
			DaysPassed ++ ;
			TriggerEvent();
			return;
		}

		Time.timeScale = 0.0f;
		messages.text = TextPerDay[DaysPassed][DayIndex];

		accept.gameObject.SetActive(true);
		messages.gameObject.SetActive (true);
		panel.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.J)) {
			StartHour = 23;
			StartMinute = 59.9;
		}

		if ( Time.timeScale == 0.0 )
			return;

		if ( DaysPassed == 0 ) {
			DayEvent();
			return;
		}

		StartMinute += Time.deltaTime * GameMinutesPerWorldSeconds;
		while ( StartMinute >= 60.0 ) {
			StartHour ++ ;
			StartMinute -= 60.0;
			if ( StartHour == 24 ) {
				StartHour = 0;
				StartDay ++ ;
				if (StartDay < 24){

					DayEvent();

				} else {
					print ( "WORKING" );
					Time.timeScale = 0.0f;
					if ( tree.gameObject.transform.localScale.y >= limit ){
						print ( "WIN" );
						win.gameObject.SetActive(true);
					} else {
						print ( "FAIL" );
						fail.gameObject.SetActive(true);
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
		if ( DayIndex+1 >= TextPerDay[DaysPassed].Length ) {
			Time.timeScale = 1.0f;
			accept.gameObject.SetActive(false);
			messages.gameObject.SetActive(false);
			panel.SetActive(false);
			DayIndex = 0;
			DaysPassed ++ ;
			TriggerEvent();
		} else {
			DayIndex ++ ;
			DayEvent();
		}
	}


}
