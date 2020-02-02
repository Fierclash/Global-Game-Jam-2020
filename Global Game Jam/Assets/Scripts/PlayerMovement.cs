using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
PlayerMovement:
	Allows the player to move within the scene
	Limits player movement from moving into illegal spaces
	If the player moves into a broken space and has enough materials:
		Repair the space

Move(Vector2Int)
*/
public class PlayerMovement : MonoBehaviour
{
	[HideInInspector] public GroundGrid grid;
	private bool canMove = true;
	public Vector2Int gridPosition;
	public int materialsCount = 15;
	public Rigidbody2D body;

	void Start()
	{
		grid = FindObjectOfType<GroundGrid>();
		TextManager.instance.UpdateMaterials(materialsCount);
	}

	void Update()
	{
		// Determines which direction the player wants to move
		int x = (int)Input.GetAxisRaw("Horizontal");
		int y = (int)Input.GetAxisRaw("Vertical");

		// Moves the player if possible
		// Limits movement to once per key press
		if(canMove)
		{
			if(y != 0)
				Move(new Vector2Int(0, y));
			if(x != 0)
				Move(new Vector2Int(x, 0));
		}
		else if(!Input.anyKey)
			canMove = true;

	}

	void FixedUpdate() {
		Vector3 currentPos = new Vector3(gridPosition.x, gridPosition.y);
		body.velocity = (currentPos - transform.position)*15;
		Debug.Log(currentPos.ToString() + "\n" + transform.position.ToString());
	}

	void Move(Vector2Int newPosition)
	{ 
		if (newPosition.x > 0) {
			GetComponent<SpriteRenderer>().flipX = true;
		} else {
			GetComponent<SpriteRenderer>().flipX = false;
		}

		// Calculates the projected position
		newPosition.x += gridPosition.x;
		newPosition.y += gridPosition.y;

		// Checks if player is move is legal
		if (newPosition.x >= 0 && newPosition.x < GroundGrid.gridSize.x && newPosition.y >= 0 && newPosition.y < GroundGrid.gridSize.y) {
			GroundTile newTile = grid.gameGrid[newPosition.x, newPosition.y].GetComponent<GroundTile>();
			if (newTile.currentDurability > 0) {
				gridPosition = newPosition;
				canMove = false;
		
				if(newTile.log != null)
				{
					Destroy(newTile.log);
					IncrMaterials();
				}

				// Detect grid boundaries and broken tiles
			} else {
				if(materialsCount > 0) // If the player has enough materials, repair the target tile
				{
					IncrMaterials(false);
					newTile.Repair(new Vector3(gridPosition.x, gridPosition.y, 0));
				}
				grid.availableTiles.Add(newTile);
				canMove = false;
			}
		}
	}

	void IncrMaterials(bool add = true)
	{
		if(add)
			materialsCount++;
		else
			materialsCount--;
		TextManager.instance.UpdateMaterials(materialsCount);
	}
}
