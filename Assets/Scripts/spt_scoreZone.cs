using UnityEngine;
using System.Collections;

public class spt_scoreZone : MonoBehaviour {

    public int scoreValue = 10;
    public int particleEmissionRate = 150;

    private ParticleSystem myParticleSystem;
    private ParticleSystem.EmissionModule emissionModule;

    void Start() {
        myParticleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        emissionModule = myParticleSystem.emission;
        emissionModule.rateOverTime = particleEmissionRate;
    }

    //When the ball falls through the hole, increase score by scoreValue
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "skeeBall") {
            spt_scoreKeeper.score += scoreValue;
            if(GetComponent<ParticleSystem>() != null) GetComponent<ParticleSystem>().Play();
            if (transform.GetChild(0).GetComponent<ParticleSystem>()) myParticleSystem.Play();
            if (GetComponent<AudioSource>() != null) GetComponent<AudioSource>().Play();
        }
    }
}