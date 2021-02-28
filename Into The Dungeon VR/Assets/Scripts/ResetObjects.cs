using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ResetObjects : MonoBehaviour
{
    public List<GameObject> itemsToReset;
    public List<GameObject> enemiesToReset;
    private Vector3[] resetItemPosition;
    private Quaternion[] resetItemRotation;

    private Transform[] resetEnemyPosition;
    // Start is called before the first frame update
    void Start()
    {
        resetItemPosition = new Vector3[itemsToReset.Count];
        resetItemRotation = new Quaternion[itemsToReset.Count];
        int i;
        for (i = 0; i < itemsToReset.Count; i++)
        {
            resetItemPosition[i] = itemsToReset[i].transform.position;
            resetItemRotation[i] = itemsToReset[i].transform.rotation;
        }
        resetEnemyPosition = new Transform[enemiesToReset.Count];
        for (i = 0; i < enemiesToReset.Count; i++)
        {
            resetEnemyPosition[i] = enemiesToReset[i].transform;
        }

    }

    public void Reset()
    {
        Debug.Log("Objects resetted");
        int i = 0;
        foreach (var item in itemsToReset)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.position = resetItemPosition[i];
            item.transform.rotation = resetItemRotation[i];
            i++;
        }

        i = 0;
        foreach (var enemy in enemiesToReset)
        {
            enemy.transform.position = resetEnemyPosition[i].position;
            enemy.transform.rotation = resetEnemyPosition[i].rotation;
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            enemyAI.enemyHealth = enemyAI.maxEnemyHealth;
            enemy.SetActive(true);
            i++;
        }

    }
}
