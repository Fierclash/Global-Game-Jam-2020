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
    [HideInInspector] public GameObject tempFloor;

    public GameObject floorTemplate;

    public int animationSpeed = 10;

    void Start()
    {
        currentDurability = 1;
    }

    void FixedUpdate() {
        if(log != null) {
            log.GetComponent<Rigidbody2D>().velocity = (transform.position - log.transform.position) * animationSpeed;
        }
        if(tempFloor != null) {
            tempFloor.GetComponent<Rigidbody2D>().velocity = (transform.position - tempFloor.transform.position) * animationSpeed;
            if((transform.position - tempFloor.transform.position).sqrMagnitude < 0.001){
                Destroy(tempFloor);
                GetComponent<SpriteRenderer>().sprite = ground;
            }
        }
    }

    public void Repair(Vector3 position) // Argument for a new GroundObject?
    {
        Debug.Log(position.ToString());
        tempFloor = Instantiate(floorTemplate, position, Quaternion.identity);
        currentDurability = 1;
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
            TextManager.instance.FixingThemBoards();
            Time.timeScale = 0f;
        }

    }

}