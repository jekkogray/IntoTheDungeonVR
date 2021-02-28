using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public static int scoreValue = 0;
    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        GetComponent<Text>().text = "Score: " + scoreValue;
    }
}
