using UnityEngine;
using System.Collections;

public class CountDisplay : MonoBehaviour {

	public uint count = 0;
	MeshRenderer self;
	TextMesh friend;

	void Start() {
		self = GetComponent<MeshRenderer>();
		friend = gameObject.transform.GetChild(0).GetComponent<TextMesh>();
		if ( friend == null ) {
			Debug.Log( "SOMETHING BROKE!!!!" );
		}
		friend.text = "x" + count.ToString();
		if ( count == 0 ) {
			self.enabled = false;
			friend.text = "";
		}
	}

	public void Increment() {
		if ( count == 0 )
			self.enabled = true;
		count ++ ;
		friend.text = "x" + count.ToString();
	}
	public void Decrement() {
		if ( count > 0 )
			count -- ;
		if ( count == 0 ) {
			self.enabled = false;
			friend.text = "";
		} else
			friend.text = "x" + count.ToString();
	}
}
