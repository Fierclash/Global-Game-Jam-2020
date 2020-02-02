﻿using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public class BoardManager : MonoBehaviour
{
    // Using Serializable allows us to embed a class with sub properties in the inspector.
    [Serializable]
    public class Count
    {
        public int minimum; 			//Minimum value for our Count class.
        public int maximum; 			//Maximum value for our Count class.
        
        
        //Assignment constructor.
        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    
    
    public int columns = 8; 										//Number of columns in our game board.
    public int rows = 8;											//Number of rows in our game board.
    public Count cannonCount = new Count (1, 5);						//Lower and upper limit for our random number of food items per level.
    public GameObject[] floorTiles;									//Array of floor prefabs.
    public GameObject[] cannonTiles;									//Array of enemy prefabs.
    public GameObject[] outerWallTiles;								//Array of outer tile prefabs.
    
    private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
    private List <Vector3> activeTiles = new List <Vector3> ();	//A list of possible locations to place tiles.
    
    public GameObject[,] gameGrid;                              // Array of the ground grid
    
    //Clears our list activeTiles and prepares it to generate a new board.
    void InitialiseList ()
    {
        //Clear our list activeTiles.
        activeTiles.Clear ();
        
        //Loop through x axis (columns).
        for(int x = 1; x < columns-1; x++)
        {
            //Within each column, loop through y axis (rows).
            for(int y = 1; y < rows-1; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                activeTiles.Add (new Vector3(x, y, 0f));
            }
        }
    }
    /*
    Grid System:
    Have both a grid and a list of active tiles

    Player:
        Move onto an index (x,y)
        Check index in matrix

        Each tile has health
        0 health is consider broken and remove from the list of active and add to list of broken tiles
    */

    
    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup ()
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject ("Board").transform;
        
        //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
        for(int x = -1; x < columns + 1; x++)
        {
            //Loop along y axis, starting from -1 to place floor or outerwall tiles.
            for(int y = -1; y < rows + 1; y++)
            {
                //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
                
                //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                if(x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
                
                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
                
                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent (boardHolder);
            }
        }
    }
    
    
    //RandomPosition returns a random position from our list activeTiles.
    Vector3 RandomPosition ()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List activeTiles.
        int randomIndex = Random.Range (0, activeTiles.Count);
        
        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List activeTiles.
        Vector3 randomPosition = activeTiles[randomIndex];
        
        //Remove the entry at randomIndex from the list so that it can't be re-used.
        activeTiles.RemoveAt (randomIndex);
        
        //Return the randomly selected Vector3 position.
        return randomPosition;
    }
    
    
    //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
    {
        //Choose a random number of objects to instantiate within the minimum and maximum limits
        int objectCount = Random.Range (minimum, maximum+1);
        
        //Instantiate objects until the randomly chosen limit objectCount is reached
        for(int i = 0; i < objectCount; i++)
        {
            //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
            Vector3 randomPosition = RandomPosition();
            
            //Choose a random tile from tileArray and assign it to tileChoice
            GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
            
            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }
    
    
    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene ()
    {
        //Creates the outer walls and floor.
        BoardSetup();
        
        //Reset our list of activeTiles.
        InitialiseList ();
        
        //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom (cannonTiles, cannonCount.minimum, cannonCount.maximum);
    }
}