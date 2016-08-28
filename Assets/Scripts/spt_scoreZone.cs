using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class spt_scoreZone : MonoBehaviour {

    public int scoreValue = 10;

    //When the ball falls through the hole, increase score by scoreValue
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "skeeBall") {
            spt_scoreKeeper.score += scoreValue;
        }
    }
    //When the ball falls through the hole, accelerate its despawn time
    void OnTriggerStay(Collider col){
        if (col.gameObject.tag == "skeeBall")
        {
            col.gameObject.GetComponent<spt_skeeBall>().timer = col.gameObject.GetComponent<spt_skeeBall>().respawnTime - .1f;
        }
        else {
            if (col.gameObject.tag == "classic")
            {
                spt_scoreKeeper.timeTrialMode = false;
                spt_scoreKeeper.infiniteMode = false;
            }
            if (col.gameObject.tag == "time")
            {
                spt_scoreKeeper.timeTrialMode = true;
                spt_scoreKeeper.infiniteMode = false;
            }
            if (col.gameObject.tag == "infinite")
            {
                spt_scoreKeeper.timeTrialMode = false;
                spt_scoreKeeper.infiniteMode = true;
            }
            SceneManager.LoadScene("test");
        } 
        
    }
}