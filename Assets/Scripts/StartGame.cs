using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour {

	public Button init_game;
	public Image init_screen;

	void Start () {
		Time.timeScale = 0.0f;
		init_screen.gameObject.SetActive (true);
		init_game.gameObject.SetActive (true);
		init_game.onClick.AddListener (delegate () {
			this.ButtonClicked ();
		});
	}
	


	void Update () {
	
	}

	public void ButtonClicked(){
		Time.timeScale = 1.0f;
		init_screen.gameObject.SetActive (false);
		init_game.gameObject.SetActive (false);
	}
}
