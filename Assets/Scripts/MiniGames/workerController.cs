using UnityEngine;
using System.Collections;

public class workerController : MonoBehaviour {

	public bool isSmacked = false;                 //use this to hook into the resource generation
	private Color startColor;
	public int chanceOfSpawning = 0;
    private int health = 0;
    Color damping = Color.grey;

	IEnumerator wait() {
		for (float f = 0.0f; f <= 1f; f += 1f) {
			yield return new WaitForSeconds(1);
		}
	}

	public void getWhipped() {
        if (health > 0) {
            health -= 1;
            damping.r = ( ( health / 10f) + 1) / 2 ;
            GetComponent<Renderer>().material.color = damping;
            print(health);
        }
        if (health == 0) {
            returnToNeutral();
        }
	}

    void returnToNeutral() {
        gameObject.GetComponent<MiniGameActive>().IsActive = true;
        isSmacked = false;
        GetComponent<Renderer>().material.color = Color.grey;
    }

	void Start () {
		startColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.grey;
	}
	
	void Update () {
		int rand = Random.Range(0, chanceOfSpawning);
		if (rand == 1) {
            isSmacked = true;
            gameObject.GetComponent<MiniGameActive>().IsActive = false;
            health = 10;
            damping.r = 1;
            GetComponent<Renderer>().material.color = damping;
		}
	}
}