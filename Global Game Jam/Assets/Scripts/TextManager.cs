using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
TextManager:
	Manages the UI text elements

Functions:
	UpdateScore(int)
	UpdateMaterials(int)
*/
public class TextManager : MonoBehaviour
{
	public TextMeshProUGUI score;
	public TextMeshProUGUI materials;

	public static TextManager instance;

    public TextMeshProUGUI hiScore;
    public TextMeshProUGUI hScore;
    public GroundGrid groundgrid;



    // Singleton
    void Awake()
	{
		if(instance == null)
			instance = this;
		else
			Destroy(this);
	}
//boop
	void Start()
	{
		UpdateScore(0);
		UpdateMaterials(0);

        FixingThemBoards();


        hScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

	// Updates the score UI
	public void UpdateScore(int newScore)
	{
		score.text = "Score: " +  newScore;
	}

	// Updates the materials UI
	public void UpdateMaterials(int count)
	{
		materials.text = "Materials: " + count;
	}

    public void FixingThemBoards()
    {

        int number = groundgrid.keepScore;
//        Debug.Log(number);
        hiScore.text = "High Score: " + number.ToString();

        // if new score is higher, change
        Debug.Log(PlayerPrefs.GetInt("HighScore", 0));
        if (number > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", number);
            hScore.text = "High Score: " + number.ToString();
        }
        
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        hScore.text = "0";
    }



}
