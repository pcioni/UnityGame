using UnityEngine;
using System.Collections;

public class UIReq : MonoBehaviour {

	public InfoDisplay obj;

	public double SpawnRate = 10.0;

	public double TimeUntilNext = 0.0;
	public string FactoryName = "New Factory";
	[TextArea(3,10)]
	public string Description = "Talk about yourself!";
	[TextArea(3,10)]
	public string FlavorText = "Talk about yourself SOME MORE!";
	
	CountDisplay SpawnObject = null;

	public void Spawn() {
		if ( SpawnObject != null )
			SpawnObject.Increment();
		else
			Debug.Log ( "SOMETHING BROKE" );
	}

	void Start() {
		if ( obj == null )
			obj = GameObject.Find( "UIDisplay" ).GetComponent<InfoDisplay>();
		TimeUntilNext = SpawnRate;
		SpawnObject = GetComponentInChildren<CountDisplay>();
	}
	void Update() {
		TimeUntilNext -= Time.deltaTime;
		if ( TimeUntilNext <= 0.0 ) {
			TimeUntilNext += SpawnRate;
			Spawn();
		}
	}

	void OnMouseEnter() {
		obj.Enable( this );
	}
	void OnMouseExit() {
		obj.Enable( null );
	}

}
