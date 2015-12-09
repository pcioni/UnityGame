using UnityEngine;
using System.Collections;

public class ChooseMaterial : MonoBehaviour {

    public Material[] matList = new Material[5];
    private Transform parentTransform;
    private Vector3 baseScale;
    private Vector3 myScale;

	// Use this for initialization
	void Start () {
        MeshRenderer tmp = GetComponent<MeshRenderer>();
        float k = Random.value * 4.99f;
        print((int)k);
        tmp.material = matList[(int) k];
        parentTransform = transform.parent.transform;
        baseScale = parentTransform.localScale;
        myScale = transform.localScale;
	}

    void Update() {
        Transform tr = transform.parent.transform;

        Vector3 locsc = tr.localScale;
        Vector3 nscale = new Vector3();
        
        nscale.x = baseScale.x / locsc.x * myScale.x;
        nscale.y = baseScale.y / locsc.y * myScale.y;
        nscale.z = baseScale.z / locsc.z * myScale.z;
        
        transform.localScale = nscale;
    }
}
