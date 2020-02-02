using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    // Start is called before the first frame update
    void Start ()
    {
        //Call the Start function of the MovingObject base class.
        base.Start ();
    }

    //AttemptMove overrides the AttemptMove function in the base class MovingObject
    //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
    protected override void AttemptMove <T> (int xDir, int yDir)
    {
        //Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
        base.AttemptMove <T> (xDir, yDir);
        
        //Hit allows us to reference the result of the Linecast done in Move.
        RaycastHit2D hit;
    }

    //OnCantMove overrides the abstract function OnCantMove in MovingObject.
    //It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
    protected override void OnCantMove <T> (T component)
    {
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;  	//Used to store the horizontal move direction.
        int vertical = 0;		//Used to store the vertical move direction.

        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
        
        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int) (Input.GetAxisRaw ("Vertical"));
        
        //Check if moving horizontally, if so set vertical to zero.
        if(horizontal != 0)
        {
            vertical = 0;
        }
        
        //Check if we have a non-zero value for horizontal or vertical
        if(horizontal != 0 || vertical != 0)
        {
            //Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
            //Pass in horizontal and vertical as parameters to specify the direction to move Player in.
            AttemptMove<Hole> (horizontal, vertical);
        }
    }
}
