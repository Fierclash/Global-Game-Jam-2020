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
    public bool hasItem;
    public Sprite ground;   //ground piece sprite
    public Vector2 position;            // The position in the grid
    public int currentDurability;      // Determines if the player can/cannot walk on the tile
    [HideInInspector] public GameObject log;

    void Start()
    {
        Repair(); // Initializes the tile's durability
    }

    void FixedUpdate() {
        if(log != null) {
            log.GetComponent<Rigidbody2D>().velocity = (transform.position - log.transform.position) * 10;
        }
    }

    public void Repair() // Argument for a new GroundObject?
    {
        currentDurability = 1;
        GetComponent<SpriteRenderer>().sprite = ground;
    }

    public bool DecrDurability()
    {
        //Debug.Log("Damaging a Tile");

        if(currentDurability > 0)
        {
            currentDurability--;
            if(currentDurability == 0)
            {
                return true;
            }
        }

        return false;
    }

    public void Break(){
        GetComponent<SpriteRenderer>().sprite = broken;
        Destroy(log);

        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if(player == null) // First must error check if there is a player
        	return;

        if(player.gridPosition == position)
        {
        	Destroy(player.gameObject);
        	GeneralManager.instance.ShowLossScreen();
        	Time.timeScale = 0f;
        }

    }

}