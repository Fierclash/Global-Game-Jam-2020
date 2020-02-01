using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
GroundGrid:
	Script to manage ground tiles within a grid configuration

Notes:
	Game Grid: 2D array internally maintained in scripts 
	Scene Grid: The configuration of ground tiles within the scene itself
	Grid must communicate with the game object's positions
	Player's position must interact with the grid
	Tiles must be directly addressed to affect its state

Functions:
	InitGrid()
	SetGrid()
*/
public class GroundGrid : MonoBehaviour
{
	public static Vector2 gridSize = new Vector2(7, 7);			// Global variable of the grid dimensions
	public static float gridSpacing = 1f;						// Space between each point within the scene

	[Header("Grid")]
	public GameObject baseTile;									// Base Tile to initialize the grid with
	public GameObject[,] gameGrid;						 		// Array of the ground grid

	void Start()
	{
		InitGrid();
		SetGrid();
	}


	// Initializes the gameGrid with gameObjects of ground tiles
	void InitGrid()
	{
		gameGrid = new GameObject[10, 10];

		for(int i=0; i < gridSize.x; i++)
		{
			for(int j=0; j < gridSize.y; j++)
			{
				Debug.Log("Iteration " + i + " " + j);

				GameObject newTile = Instantiate(baseTile);
				GroundTile groundTile = newTile.GetComponent<GroundTile>();

				/*
				if(groundTile == null)
				{
					Debug.Log("Could not find GroundTile");
					continue;
				}
				*/

				if(newTile == null)
				{
					Debug.Log("Could not find newTile");
					continue;
				}

				groundTile.position = new Vector2(i,j);
				gameGrid[i,j] = newTile;
			}
		}
	}

	// Sets up the grid in the scene
	void SetGrid()
	{
		// Goes through the 2D array for each tile and sets its position in the scene grid
		for(int i=0; i<gridSize.x; i++) // 10
		{
			for(int j=0; j<gridSize.y; j++) // 10
				gameGrid[i,j].transform.position = gameGrid[i,j].GetComponent<GroundTile>().position * GroundGrid.gridSpacing;
		}
	}
}
