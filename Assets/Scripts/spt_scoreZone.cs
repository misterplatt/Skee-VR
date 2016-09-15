using UnityEngine;
using System.Collections;

public class spt_scoreZone : MonoBehaviour {

    public int scoreValue = 10;
    public float particleEmissionDuration = 0.3f;
    public int particleEmissionRate = 150;
    public float particleEmissionRadius = 0.05f;

    private ParticleSystem myParticleSystem;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.ShapeModule shapeModule;

    void Start() {
        myParticleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        emissionModule = myParticleSystem.emission;
        emissionModule.rateOverTime = particleEmissionRate;
        shapeModule = myParticleSystem.shape;
        shapeModule.radius = particleEmissionRadius;
        mainModule = myParticleSystem.main;
        mainModule.duration = 1f;
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