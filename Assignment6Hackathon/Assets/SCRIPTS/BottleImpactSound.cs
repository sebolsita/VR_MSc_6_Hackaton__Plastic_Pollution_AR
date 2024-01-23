using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleImpactSound : MonoBehaviour
{
    public AudioSource impactSound;
    public AudioClip[] impactSounds; // Array to hold your sound clips

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            int randomIndex = Random.Range(0, impactSounds.Length); // Select a random index
            impactSound.clip = impactSounds[randomIndex]; // Set the clip to the randomly selected one
            impactSound.Play(); // Play the sound
        }
    }
}
