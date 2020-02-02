using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOld : MonoBehaviour
{

    public Rigidbody2D rigidbody;
    float movementSpeed = 40f;

    int movement;
    Vector2Int position;
    GroundTile ground;

    GameObject[,] grid;

    bool movementCooldown;

    // Start is called before the first frame update

    public void InitPlayer(GameObject[,] grid, Vector2Int position) {
        this.grid = grid;
        this.position = position;
        GroundTile ground = grid[position.x, position.y].GetComponent<GroundTile>();
        this.ground = ground;
        rigidbody.transform.position = ground.transform.position;
        movementCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0) { // left
            movement = 1;
        } else if (Input.GetAxisRaw("Horizontal") > 0) { // right
            movement = 2;
        } else if (Input.GetAxisRaw("Vertical") > 0) { // up
            movement = 3;
        } else if (Input.GetAxisRaw("Vertical") < 0) { // down
            movement = 4;
        } else { // not moving
            movement = 0;
        }
    }

    void FixedUpdate() {
        if (!movementCooldown) {
            Move(movement);
        }
    }
    
    void Move(int direction) {
        if (validMove(direction)) {
            if (direction == 1) { // left
                position.x -= 1;
            } else if (direction == 2) { // right
                position.x += 1;
            } else if (direction == 3) { // up
                position.y += 1;
            } else if (direction == 4) { // down
                position.y -= 1;
            }
            Debug.Log(position.ToString());
            GroundTile ground = grid[position.x, position.y].GetComponent<GroundTile>();
            this.ground = ground;
            rigidbody.transform.position = ground.transform.position; // will be changed to an animation
            movementCooldown = true;
            new WaitForSeconds(3);
            movementCooldown = false;
        }
    }

    bool validMove(int direction) {
        if (direction == 1) { // left
            return position.x > 0;
        } else if (direction == 2) { // right
            return position.x < (grid.GetLength(0) - 1);
        } else if (direction == 3) { // up
            return position.y < (grid.GetLength(1) - 1);
        } else if (direction == 4) { // down
            return position.y > 0;
        }
        return false;
    }
}
