using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardManager;

    void Awake()
    {
        boardManager = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame() {
        boardManager.SetupScene();
    }
}
