using UnityEngine;
using System.Collections;

public class CreateDraggable : MonoBehaviour {

	public GameObject tocreate;
	private CountDisplay self = null;

	// Use this for initialization
	void Start () {
		self = GetComponent<CountDisplay>();
	}

	void OnMouseDown() {
		if ( self.count != 0 ) {
			self.Decrement();
			GameObject obj = Instantiate( tocreate );
			obj.transform.position = transform.position;
		}
	}
}
