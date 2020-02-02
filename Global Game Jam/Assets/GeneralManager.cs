using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
	public static GeneralManager instance;
	public GameObject lossScreen;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else
			Destroy(this);

		lossScreen.SetActive(false);
	}

	public void ShowLossScreen(bool willShow = true)
	{
		lossScreen.SetActive(willShow);
	}
}
