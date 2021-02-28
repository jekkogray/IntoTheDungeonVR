using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 50;
    public GameObject bullet;
    public int gunDamage = 10;
    public Transform barrel;
    public AudioClip gunSoundAudioClip;

    private void Start()
    {
        gameObject.AddComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
        BulletBehavior bulletBehavior = spawnedBullet.GetComponent<BulletBehavior>();
        bulletBehavior.sourceSpawn = gameObject;
        bulletBehavior.bulletDamage = gunDamage;
        
        spawnedBullet.tag = "Player Bullet";
        GetComponent<AudioSource>().PlayOneShot(gunSoundAudioClip);
    }
}
