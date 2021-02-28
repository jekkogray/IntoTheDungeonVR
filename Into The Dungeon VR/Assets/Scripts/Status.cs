using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    // Start is called before the first frame update
    public static int playerHealth;
    public static int maxPlayerHealth = 1000;
    void Update()
    {
        UpdateScore();
    }
    private void Start()
    {
        playerHealth = maxPlayerHealth;
    }

    void UpdateScore()
    {
        GetComponent<Text>().text = "Health: " + playerHealth + "/" + maxPlayerHealth;
    }
}
