using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Status.playerHealth -= collision.gameObject.GetComponent<EnemyAI>().enemyDamage;
            if (Status.playerHealth <= 0)
            {
                Debug.Log("Player is dead!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Debug.Log(collision.gameObject.name);
            Debug.Log("Damage: -" + collision.gameObject.GetComponent<EnemyAI>().enemyDamage);
            Debug.Log("Player Health: " + Status.playerHealth + "/" + Status.maxPlayerHealth);
            StartCoroutine("ResetDamage");
        }

        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
        }

        if (collision.gameObject.tag == "Enemy Bullet") {
            Status.playerHealth -= collision.gameObject.GetComponent<BulletBehavior>().bulletDamage;
            if (Status.playerHealth <= 0)
            {
                Debug.Log("Player is dead!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Debug.Log(collision.gameObject.name);
            Debug.Log("Damage: -" + collision.gameObject.GetComponent<BulletBehavior>().bulletDamage);
            Debug.Log("Player Health: " + Status.playerHealth + "/" + Status.maxPlayerHealth);
        }
    }
        private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Status.playerHealth -= collision.gameObject.GetComponent<EnemyAI>().enemyDamage;
            if (Status.playerHealth <= 0)
            {
                Debug.Log("Player is dead!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Debug.Log(collision.gameObject.name);
            Debug.Log("Damage: -" + collision.gameObject.GetComponent<EnemyAI>().enemyDamage);
            Debug.Log("Player Health: " + Status.playerHealth + "/" + Status.maxPlayerHealth);
            StartCoroutine("ResetDamage");
        }
    }
}
