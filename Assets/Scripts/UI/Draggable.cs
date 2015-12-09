using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {

    public GameObject ornament;

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
