using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    private float timeLeft = 400;
    public float player_score = 0;
   
    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.1f)
        {
            SceneManager.LoadScene ("Main");
        }
    }

    void OnTriggerEnter2D (Collider2D trig)
    {
        Debug.Log ("Touched end of level!");
        CountScore();
    }

    void CountScore()
    {
        player_score += timeLeft * 10;
        Debug.Log (player_score);
    }
}
