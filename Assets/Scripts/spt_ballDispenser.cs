using UnityEngine;
using System.Collections;

public class spt_ballDispenser : MonoBehaviour {

    public GameObject skeeBall;
    public float spawnRate = .25f;

    private float timer = 0;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (spt_scoreKeeper.ballsToSpawn != 0) {
            timer += Time.deltaTime;
            if (timer >= spawnRate) {
                Debug.Log("SPAWNING");
                SpawnBall();
            }
        }
    }

    //Spawn a ball at respawn point, add force down gutter, decrement balls
    void SpawnBall() {
        GameObject tempBall = Instantiate(skeeBall, transform.position, Quaternion.identity) as GameObject;
        tempBall.GetComponent<Rigidbody>().AddForce(0, 0, -100);
        timer = 0;
        //if (!spt_scoreKeeper.infiniteMode)
        spt_scoreKeeper.ballsToSpawn--;
    }
}
