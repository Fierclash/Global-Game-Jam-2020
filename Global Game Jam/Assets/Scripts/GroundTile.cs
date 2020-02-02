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
    public GroundObject baseGround;     // Base stats to initialize with
    public Sprite broken;   //Broken ground piece
    public Vector2 position;            // The position in the grid
    int currentDurability;      // Determines if the player can/cannot walk on the tile

    void Start()
    {
        Repair(); // Initializes the tile's durability
    }

    void Repair() // Argument for a new GroundObject?
    {
        currentDurability = 1;
    }

    public bool DecrDurability()
    {
        Debug.Log("Damaging a Tile");

        if(currentDurability > 0)
        {
            currentDurability--;
            
            if(currentDurability == 0)
            {
                GetComponent<SpriteRenderer>().sprite = broken;
                return true;
            }
        }

        return false;
    }
}