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
						"In the center of Town, the villagers plant a small tree, and begin their yearly tradition. Each year, the villagers of Christmas Town decorate their tree with lights, ornaments, and other decorations.",
						"As more decorations go on the tree, the tree will grow, spreading Christmas cheer across the world!",
						"This year, it's your turn to be in charge of the tree's growth. Christmas Town is depending on you, so spread Christmas cheer far and wide!" }, 
		new string[] {	"One day has passed, and your tree is already showing signs of growth! Each day, you will receive a gift from those with Christmas Cheer!",
						"Today, you received a bundle of Ornaments and a Stocking!" },
		new string[] {	"As the villagers of Christmas Town wake up for another busy day, they are delighted to see that the tree is now taller than you! Up until now, you have only been able to decorate your tree with Ornaments, but you hear that the Light Factory is set to reopen tomorrow. Opening today's gift, you are delighted to see another bundle of Ornaments!" },
		new string[] {	"You wake up to a surprise - the tree is now the size of the Town Square! As you begin preparations to continue to decorate the tree, one of the villagers comes running towards you. Bad news! The Light Factory is having problems! Go check it out! Inside your daily gift box, you find a Generator! You will be able to plug your Lights into the Generator to provide light to the tree!",
						"The head of the Light Factory thanks you for your trouble, and presents you with a string of Lights! Starting tomorrow, the Factory will provide you with a daily supply of Lights to put on the tree!" },
		new string[] { "GOODBYE" },
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
		new string[] {},
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
	
	public Text messages;
	public Button accept;
	public GameObject panel;
	
	private Text timer;

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
	private void DayEvent(){
		if ( TextPerDay.Length <= DaysPassed || TextPerDay[DaysPassed].Length <= DayIndex ) {
			return;
		}

		Time.timeScale = 0.0f;

		print ( DaysPassed );
		print ( DayIndex );
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

				DayEvent();

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
		} else {
			DayIndex ++ ;
			DayEvent();
		}
	}


}
