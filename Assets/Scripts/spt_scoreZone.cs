using UnityEngine;
using System.Collections;

public class spt_scoreZone : MonoBehaviour {

    public int scoreValue = 10;

    //When the ball falls through the hole, increase score by scoreValue
    void OnTriggerEnter(Collider col) {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "skeeBall") {
            spt_scoreKeeper.score += scoreValue;
        }
    }
}