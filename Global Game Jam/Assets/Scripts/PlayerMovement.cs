using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public GroundGrid grid;
	private bool canMove = true;
	public Vector2Int gridPosition;

	void Start()
	{
		grid = FindObjectOfType<GroundGrid>();
	}

	void Update()
	{
		/*
		if(Input.GetAxisRaw("Horizontal") != Input.GetAxisRaw("Vertical") && Input.anyKey && canMove)
			Move(new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical")));
		else if(!Input.anyKey)
		{
			canMove = true;
		}
		*/

		int x = (int)Input.GetAxisRaw("Horizontal");
		int y = (int)Input.GetAxisRaw("Vertical");
		if(canMove)
		{
			if(y != 0)
				Move(new Vector2Int(0, y));
			else if(x != 0)
				Move(new Vector2Int(x, 0));
		}
		else if(!Input.anyKey)
		{
			canMove = true;
		}

	}

	void Move(Vector2Int newPosition)
	{ 
		newPosition.x += gridPosition.x;
		newPosition.y += gridPosition.y;
		if (newPosition.x >= 0 && newPosition.x < GroundGrid.gridSize.x && newPosition.y >= 0 && newPosition.y < GroundGrid.gridSize.y) {
			if (grid.gameGrid[newPosition.x, newPosition.y].GetComponent<GroundTile>().currentDurability > 0) {
				gridPosition = newPosition;
				transform.position = new Vector3Int(newPosition.x, newPosition.y, 0);
				canMove = false;
		
				// Detect grid boundaries and broken tiles
			}
		}
	}
}
