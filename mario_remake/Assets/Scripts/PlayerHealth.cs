using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool hasDied;
    // Start is called before the first frame update
    void Start()
    {
        hasDied = false; //Starts the scene with the player not dead
    }

    // Update is called once per frame
    void Update()
    {
        if ( gameObject.transform.position.y < -8.1){ // if the player falls below y= -8.1, the player dies
            Die ();
        }

    }

    void Die ()
    {
        SceneManager.LoadScene("Main"); // when the player dies, reloads the Main scene
        
    }
}
