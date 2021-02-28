using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreEnemiesDead : MonoBehaviour
{
    List<GameObject> listOfEnemies;
    public AudioClip nextLevelCue;
    private static int enemyTotal;
    // Start is called before the first frame update
    void Start()
    {
        listOfEnemies = new List<GameObject>();
        listOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemyTotal = listOfEnemies.Count;
        Debug.Log("Enemies total: " + listOfEnemies.Count + "/" + enemyTotal);
    }
    void Update()
    {
    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(nextLevelCue.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Summary:
    // Update dead enemy
    public void KilledEnemy(GameObject enemy)
    {
        if (listOfEnemies.Contains(enemy))
        {
            listOfEnemies.Remove(enemy);
        }
        Debug.Log("Enemies total: " + listOfEnemies.Count + "/" + enemyTotal);
    }
    public void EnemiesDead()
    {
        if (listOfEnemies.Count == 0)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.55f;
            audioSource.clip = nextLevelCue;
            audioSource.Play();
            StartCoroutine("GoToNextLevel");
        }
    }

}
