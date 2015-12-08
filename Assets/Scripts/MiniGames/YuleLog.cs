using UnityEngine;
using System.Collections;

public class YuleLog : MonoBehaviour {

	public float CheckInterval = 3.0f;
	public float CheckPossibility = 0.05f;
	public float HeatLoss = 0.05f;
	public float HeatIncrease = 0.01f;
	public Renderer display = null;

	private objectHighlightOnMouseover mask;
	private bool lit = true;
	private float heat = 0.0f;
	private Vector3 lastPos = new Vector3();

	// Use this for initialization
	void Start () {
		mask = GetComponentInParent<objectHighlightOnMouseover>();
		if ( mask == null )
			Debug.Log( "Yule Log is not parented to an object that has objectHighlightOnMouseover" );
		if ( display == null ) {
			display = (Renderer) GetComponent<MeshRenderer> ();
		}
		SetObjectLit();
	}

	public void SetObjectLit() {
		display.enabled = true;
		StartCoroutine( CheckDeactivate() );
	}
	public void SetObjectUnlit() {
		heat = 0f;
		display.enabled = false;
	}
	
	void OnMouseOver() {
		if ( Time.timeScale == 0.0f )
			return;
		Vector3 delta = Input.mousePosition - lastPos;
		float add = HeatIncrease * Mathf.Sqrt( delta.x * delta.x + delta.y * delta.y );
		heat += add;
	}
	void Update () {
		if ( !lit ) {
			print ( heat );
			if ( heat >= 1f ) {
				lit = true;
				SetObjectLit();
			} else
				heat *= ( 1 - HeatLoss * Time.timeScale );
		}
		lastPos = Input.mousePosition;
	}

	private void Deactivate() {
		lit = false;
		SetObjectUnlit();
	}
	private IEnumerator CheckDeactivate() {
		while ( lit ) {
			yield return new WaitForSeconds( CheckInterval );
			if ( Random.value < CheckPossibility )
				Deactivate();
		}
	}
}
