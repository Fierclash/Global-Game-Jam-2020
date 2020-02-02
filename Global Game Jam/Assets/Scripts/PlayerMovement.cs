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
		if(Input.anyKey && canMove)
		{
			Move(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0));
		}
		else if(!Input.anyKey)
		{
			canMove = true;
		}
	}

	void Move(Vector3 newPosition)
	{
		transform.position = transform.position + newPosition;
		canMove = false;
	
		// Detect grid boundaries and broken tiles
	}
}
