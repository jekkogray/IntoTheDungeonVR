using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [Range(0, 100)] public int bulletDamage = 5;
    public GameObject sourceSpawn;
    public GameObject particleEffect;
    public AudioClip onHitAudioClip;
    private void Start()
    {
        Physics.IgnoreCollision(sourceSpawn.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
        // GetComponent<ParticleSystem>().Stop();
        // Destroy after 10 seconds.
        Destroy(gameObject, 5);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision: " + collision.gameObject.name);
        GameObject explosion = Instantiate(particleEffect, collision.GetContact(0).point, particleEffect.transform.rotation);

        explosion.GetComponent<ParticleSystem>().Play();

        if (onHitAudioClip != null)
        {
            explosion.AddComponent<AudioSource>().PlayOneShot(onHitAudioClip);
        }

        GetComponent<MeshRenderer>().enabled = false;
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
    private void OnCollisionExit(Collision collision){
        Destroy(gameObject);
    }
}

