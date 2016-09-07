using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class spt_levelZone : MonoBehaviour
{
    public string levelToLoad;
    
    //When the ball falls through the hole, accelerate its despawn time
    void OnTriggerStay(Collider col)
    {
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
        if (GetComponent<ParticleSystem>() != null) GetComponent<ParticleSystem>().Play(); 
        if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().Play();
        Invoke("CustomLoadScene", 1f);

    }

    void CustomLoadScene() {
        SceneManager.LoadScene(levelToLoad);
    }
}