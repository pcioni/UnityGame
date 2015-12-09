using UnityEngine;
using System.Collections;

public class workerController : MonoBehaviour {

	public bool isSmacked = false;                 //use this to hook into the resource generation
	private Color startColor;
	public int chanceOfSpawning = 0;
    private int health = 0;

	IEnumerator wait() {
		for (float f = 0.0f; f <= 1f; f += 1f) {
			yield return new WaitForSeconds(1);
		}
	}

	public void getWhipped() {
        if (health > 0) {
            health -= 1;

        }
        if (health == 0) {
            returnToNeutral();
        }
        print("whipped");
	}

    void returnToNeutral() {
        gameObject.GetComponent<MiniGameActive>().IsActive = true;
        isSmacked = false;
        GetComponent<Renderer>().material.color = startColor;
    }

	// Use this for initialization
	void Start () {
		startColor = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		int rand = Random.Range(0, chanceOfSpawning);
		if (rand == 1) {
            isSmacked = true;
            gameObject.GetComponent<MiniGameActive>().IsActive = false;
            health = 5;
			GetComponent<Renderer>().material.color = Color.red;
		}
	}
}