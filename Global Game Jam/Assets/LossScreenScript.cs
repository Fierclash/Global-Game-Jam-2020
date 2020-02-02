using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScreenScript : MonoBehaviour
{
    private Scene menuScene, currentScene;

    

    public void ResetGame()
    {
        Application.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void LeaveLevel()
    {
        Application.LoadLevel("Main Menu");
    }

}
