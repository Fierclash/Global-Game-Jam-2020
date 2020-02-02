using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

	// Singleton
	void Awake()
	{
		if(instance == null)
			instance = this;
		else
			Destroy(this);
	}

	void Start()
	{
		UpdateScore(0);
		UpdateMaterials(0);
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
}
