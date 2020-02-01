using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
GroundObject (ScriptableObject):
	Template stats for a ground tile
	Any ground tile will use a GroundObject to set its initial stats
*/
[CreateAssetMenu(fileName = "New Ground", menuName = "Ground")]
public class GroundObject : ScriptableObject
{
	public GameObject tile;
	public int durability;
}
