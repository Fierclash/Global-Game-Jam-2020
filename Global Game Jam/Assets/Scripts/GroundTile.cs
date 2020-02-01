using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
GroundTile:
	Behavior for ground tiles

Notes:
	Player can walk on the tile when it has durability
	Player cannot walk onto the tile if the durability is 0
	Player will die if:
		Player is currently on the tile AND the durability reaches to 0
*/
public class GroundTile : MonoBehaviour
{
	public GroundObject baseGround;		// Base stats to initialize with
	public Vector2 position;			// The position in the grid
	public int currentDurability;		// Determines if the player can/cannot walk on the tile

	void Start()
	{
		Repair(); // Initializes the tile's durability
	}

	void Repair() // Argument for a new GroundObject?
	{
		// Returns the currentDurability to max
	}

	public void DecrDurability()
	{
		currentDurability--;
		if(currentDurability == 0)
		{
			// Break();
		}
	}
}
