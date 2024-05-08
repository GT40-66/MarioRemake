using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{   
    public TextMeshProUGUI timeleftUI;
    public TextMeshProUGUI playerScoreUI;
    public TextMeshProUGUI coinCount;
    private float timeLeft = 400;
    public float player_score = 0;
     public CoinManager gM;
   
    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeleftUI.GetComponent<TMPro.TextMeshProUGUI>().text = ("Time: " + (int)timeLeft);
        playerScoreUI.GetComponent<TMPro.TextMeshProUGUI>().text = ("Score: " + (int)player_score);
        coinCount.GetComponent<TMPro.TextMeshProUGUI>().text = ("Coins: " + (int)gM.coinManager);
        if (timeLeft < 0.1f)
        {
            SceneManager.LoadScene ("Main");
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            CountScore();
        }
         if (other.gameObject.CompareTag("Coin"))
        {
            gM.coinManager++;
        }
        
    }
  

    void CountScore()
    {
        player_score += timeLeft * 10;
        player_score += gM.coinManager * 10;
      
    }
}
