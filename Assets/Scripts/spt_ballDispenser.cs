using UnityEngine;
using System.Collections;

public class spt_ballDispenser : MonoBehaviour {

    public GameObject skeeBall;
    public int ballsToSpawn = 5;
    public float spawnRate = .25f;

    private float timer = 0;

	// Use this for initialization
	void Start () {
        //Spawn the first ball
        spawnBall();
    }
	
	// Update is called once per frame
	void Update () {
        if (ballsToSpawn != 0) {
            timer += Time.deltaTime;
            if (timer >= spawnRate) {
                spawnBall();
            }
        }
    }

    //Spawn a ball at respawn point, add force down gutter, decrement balls
    void spawnBall() {
        GameObject tempBall = Instantiate(skeeBall, transform.position, Quaternion.identity) as GameObject;
        tempBall.GetComponent<Rigidbody>().AddForce(0, 0, -100);
        ballsToSpawn--;
        timer = 0;
        if (!spt_scoreKeeper.infiniteMode) spt_scoreKeeper.ballsRemaining--;
    }
}
