using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScreenScript : MonoBehaviour
{
    private Scene menuScene, currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

    }

    public void ResetGame()
    {
        Application.LoadLevel(currentScene.name);
    }

    public void LeaveLevel()
    {
        Application.LoadLevel(menuScene.name);
    }

}
