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

    public GameObject warning;   //Broken ground piece
    public Sprite ground;   //ground piece sprite
    public Vector2 position;            // The position in the grid
    public int currentDurability;      // Determines if the player can/cannot walk on the tile

    void Start()
    {
        Repair(); // Initializes the tile's durability
    }

    public void Repair() // Argument for a new GroundObject?
    {
        currentDurability = 1;
        GetComponent<SpriteRenderer>().sprite = ground;
    }

    public bool DecrDurability()
    {
        Debug.Log("Damaging a Tile");

        if(currentDurability > 0)
        {   
            if(currentDurability == 1)
            {
                StartCoroutine(Damage());
                return true;
            }
        }

        return false;
    }

    IEnumerator Damage(){
        GameObject warningShot = Instantiate(warning, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        currentDurability--;
        GetComponent<SpriteRenderer>().sprite = broken;
        Destroy(warningShot.gameObject);
    }
}