using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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
	public static Vector2Int gridSize = new Vector2Int(7, 7);			// Global variable of the grid dimensions
	public static float gridSpacing = 1f;						// Space between each point within the scene
    public int countTime = 0;                                   // timer to be used for update
    public bool TimeforSmash = false;                        // break tile condition
    public int cannonBallCount = 3;
    private int Rand1 = 0, Rand2 = 0;                           // random vars for tiles to smash
    //public GroundTile tilemap;                                  // groundtile variable to change between broken and fixed

    public GameObject player;

	[Header("Grid")]
	public GameObject baseTile;									// Base Tile to initialize the grid with
	public List<GroundTile> availableTiles;

	//public PlayerOld player;										// Player tile to initialize the grid with
	public GameObject[,] gameGrid;						 		// Array of the ground grid
	public float fireRate = 1f;

	public bool firing = true;

	public Vector2Int materialCount;

	public GameObject warning;   //Broken ground piece
	public GameObject logTemplate;

	void Start()
	{
		InitGrid();
		SetGrid();

		GameObject placedPlayer = Instantiate(player, Vector3.zero, Quaternion.identity);// add player
		placedPlayer.GetComponent<PlayerMovement>().gridPosition = Vector2Int.zero;

    	StartCoroutine(FireCannon());
	}

	// Initializes the gameGrid with gameObjects of ground tiles
	void InitGrid()
	{
		gameGrid = new GameObject[gridSize.x, gridSize.y];
		availableTiles = new List<GroundTile>();

		for(int i=0; i < gridSize.x; i++)
		{
			for(int j=0; j < gridSize.y; j++)
			{
				//Debug.Log("Iteration " + i + " " + j);

				GameObject newTile = Instantiate(baseTile);
				GroundTile groundTile = newTile.GetComponent<GroundTile>();
				availableTiles.Add(groundTile);

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

		///player.InitPlayer(gameGrid, new Vector2Int(2, 2));
	}

    void DestroyRandom()
    {
        // cycle through 0 to 48, select 3 tiles, break, set timer to 0
        for (int i = 0; i < cannonBallCount; i++)
        {
        	/*
            Rand1 = Random.Range(0, gridSize.x);
            Rand2 = Random.Range(0, gridSize.y);

            if(gameGrid[Rand1, Rand2])
           		gameGrid[Rand1, Rand2].GetComponent<GroundTile>().DecrDurability(); //'disable' platform until reconstructed

            gameGrid[Rand1, Rand2].GetComponent<Tilemap>();
			*/
            ////
            if(availableTiles.Count == 0 || availableTiles == null)
            {
            	Debug.LogWarning("No more available tiles");
            	return;
            }

           	int length = availableTiles.Count;
           	GroundTile target = availableTiles[Random.Range(0, length)];
			StartCoroutine(Damage(target));


           /* if (gameGrid[Rand1, Rand2].SetActive(false))
            {
                gameGrid[Rand1, Rand2].
            }
            */
        }

    }

    void FixedUpdate()
    {
    		/*
        countTime++;
        if (countTime == 60)
        {
            TimeforSmash = true;
            DestroyRandom();
            countTime = 0;
            TimeforSmash = false;
        }*/
    }

    IEnumerator FireCannon()
    {
    	while(firing)
    	{
    		DestroyRandom();
    		yield return new WaitForSeconds(1f / fireRate);
    	}
    }

	void spawnMaterials(GroundTile target){
		List<GroundTile> list = getAdjacentAvailableTiles(target);
		int numMaterials = Random.Range(materialCount.x, materialCount.y);
		for(int i = 0; i < numMaterials; i++){
			if (list.Count > 0){
				spawnLog(list[Random.Range(0, list.Count)]);
			}
		}
	}

	List<GroundTile> getAdjacentAvailableTiles(GroundTile tile) {
		List<GroundTile> list = new List<GroundTile>();
		Vector2Int position = new Vector2Int((int)tile.transform.position.x, (int)tile.transform.position.y);

		for(int x = -1; x <= 1; x+=2) {
			for(int y = -1; y <= 1; y+=2) {
				try {
					GroundTile newTile = gameGrid[position.x + x, position.y + y].GetComponent<GroundTile>();
					if (newTile.currentDurability > 0 && newTile.log == null){
						list.Add(newTile);
					}
				} catch {
				}
			}
		}
		return list;
	}

	IEnumerator Damage(GroundTile target){
        GameObject warningShot = Instantiate(warning, target.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(warningShot.gameObject);
		if(target.DecrDurability()){
			target.Break();
			spawnMaterials(target);
			availableTiles.Remove(target);
		}
    }

    public void spawnLog(GroundTile tile){
        tile.log = Instantiate(logTemplate, tile.transform.position, Quaternion.identity);
    }

}


