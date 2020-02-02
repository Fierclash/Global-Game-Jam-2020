using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScreenScript : MonoBehaviour
{
    private Scene menuScene, currentScene;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restarting");
            ResetGame();
        }
    }
    

    public void ResetGame()
    {
        Application.LoadLevel(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void LeaveLevel()
    {
        Application.LoadLevel("Main Menu");
    }

}
