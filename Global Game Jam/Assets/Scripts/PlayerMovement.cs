using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private bool canMove = true;

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
	}
}
