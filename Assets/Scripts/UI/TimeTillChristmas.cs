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
		new string[] {	"As the Sun rises over Christmas Town, a small population comes to life. The children of the village look outside - there's snow on the ground! It's Christmas Season!",
						"In the center of Town, the villagers plant a small tree, and begin their yearly tradition. Each year, the villagers of Christmas Town decorate their tree with ornaments.",
						"As more decorations go on the tree, the tree will grow, spreading Christmas cheer across the world!",
						"This year, it's your turn to be in charge of the tree's growth. Christmas Town is depending on you, so spread Christmas cheer far and wide!" }, 
		new string[] {	"One day has passed, and your tree is already showing signs of growth!" },
		new string[] { },
		new string[] { "As the villagers of Christmas Town wake up for another busy day, they are delighted to see that the tree is now taller than you!" },
		new string[] { },
		new string[] { },
		new string[] { "One week has passed. The tree has grown so tall that we can make ornaments on the moon!" },
		new string[] {},
		new string[] {},
		new string[] { "Only two weeks until Christmas Eve! Everyone is jumping with joy (at the time off)! Ornaments are now being produced on Mars!" },
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {}
	};
	public float[] RequiredSize = new float[30];
	
	public Text messages;
	public Button accept;
	public GameObject panel;
	
	private Text timer;

	public GameObject tree;
	public Image win;
	public Image fail;

	// Use this for initialization
	void Start () {
		timer = GetComponent<Text>();
		accept.gameObject.SetActive (false);
		messages.gameObject.SetActive (false);
		panel.SetActive(false);
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
			DaysPassed +=1;
			StartHour = 8;
			StartMinute = 0;
			DayEvent ();
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
				if (StartDay<25){
					DayEvent();
				}else{
					Time.timeScale = 0.0f;
					if (tree.gameObject.transform.localScale.y >= 60){
						win.gameObject.SetActive(true);
					}
					else{
						fail.gameObject.SetActive(false);
					}

				}
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
