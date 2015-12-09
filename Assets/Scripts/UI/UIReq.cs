using UnityEngine;
using System.Collections;

public class UIReq : MonoBehaviour {

	public InfoDisplay obj;
	
	private double TimeSoFar = 0.0;
	private double ScalingFactor = 1.0;
	public double TimeUntilNext = 0.0;
	public string FactoryName = "New Factory";
	[TextArea(3,10)]
	public string Description = "Talk about yourself!";
	[TextArea(3,10)]
	public string FlavorText = "Talk about yourself SOME MORE!";
	
	private CountDisplay SpawnObject = null;
	private Component[] MiniGames;

	public void Spawn() {
		if ( SpawnObject != null )
			SpawnObject.Increment();
		else
			Debug.Log ( "SOMETHING BROKE" );
	}

	void Start() {
		if ( obj == null )
			obj = GameObject.Find( "UIDisplay" ).GetComponent<InfoDisplay>();
		SpawnObject = GetComponentInChildren<CountDisplay>();
		MiniGames = GetComponentsInChildren<MiniGameActive>();
		TimeSoFar = 0.0;
	}

	void Update() {
		if ( MiniGames.Length == 0 )
			ScalingFactor = 1.0;
		else {
			int total = 0;
			foreach (MiniGameActive mini in MiniGames) {
				if ( mini.IsActive ) {
					total ++ ;
				}
			}
			ScalingFactor = (double) total / (double) MiniGames.Length;
		}
		TimeSoFar += Time.deltaTime * ScalingFactor;
		int k = 3;
		while ( k > 0 && TimeSoFar >= TimeUntilNext ) {
			print ( TimeSoFar );
			TimeSoFar -= TimeUntilNext;
			Spawn();
			k -- ;
		}
	}
	public double GetTimeToNext() {
		if ( ScalingFactor == 0.0 )
			return double.PositiveInfinity;
		else
			return ( TimeUntilNext - TimeSoFar ) / ScalingFactor;
	}

	void OnMouseEnter() {
		obj.Enable( this );
	}
	void OnMouseExit() {
		obj.Enable( null );
	}

}
