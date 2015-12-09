using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {

    public GameObject ornament;

	private Vector3 screenPoint;
	private Vector3 offset;
	private bool justCreated = true;

	// Use this for initialization
	void Start () {
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position -
				 Camera.main.ScreenToWorldPoint(
						new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)
				 );
		justCreated = true;
	}
	
	void Update() {
        if (Input.GetMouseButtonUp(0)) {
            if (GetComponent<Renderer>().enabled)
            {
                GameObject nobj = (GameObject) Instantiate(ornament);
                nobj.transform.position = transform.position;
                nobj.transform.localScale = new Vector3(10,10,10);
				GameObject g = GameObject.Find("tree");
                nobj.transform.parent = g.transform;
				g.GetComponent<Treee>().Ornaments ++ ;
				print ( g.GetComponent<Treee>().Ornaments );
            }
            Destroy(gameObject);
            return;
        }
		
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool found = false;

        if (Physics.Raycast(ray, out hit))
        {
            int id = GameObject.Find("tree").GetInstanceID();
            if (hit.transform.gameObject.GetInstanceID() == id)
            {
                transform.position = hit.point;
                GetComponent<Renderer>().enabled = true;
                found = true;
            }
        }
        if (!found)
        {
            GetComponent<Renderer>().enabled = false;
        }
	}
}
