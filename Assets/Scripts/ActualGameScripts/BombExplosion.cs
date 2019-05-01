using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombExplosion : MonoBehaviour
{
    public GameObject boatMine;
    public ParticleSystem explosionsParticles;

    void OnCollisionEnter(Collision collision) {      
        explosionsParticles.Play();
        boatMine.SetActive(false);
        Invoke("ActivateBomb", 5f);
        Invoke("StopParticles", 3f);
    }

    void ActivateBomb() {
        boatMine.SetActive(true);
    }

    void StopParticles() {
        explosionsParticles.Stop();
    }


}
