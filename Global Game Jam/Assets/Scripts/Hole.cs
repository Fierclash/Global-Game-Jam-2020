using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite built;					//Alternate sprite to display after Hole has been attacked by player.
    public int hp = 0;							//hit points for the hole.
    private SpriteRenderer spriteRenderer;		//Store a component reference to the attached SpriteRenderer.

    void Start()
    {
        //Get a component reference to the SpriteRenderer.
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    //Rebuild is called when the player rebuilds a hole.
    public void Rebuild (int loss)
    {        
        //Set spriteRenderer to the damaged wall sprite.
        spriteRenderer.sprite = built;
        
        //Subtract loss from hit point total.
        hp += loss;
        
        //If hit points are less than or equal to zero:
        if(hp > 0)
            //Disable the gameObject.
            gameObject.SetActive (false);
    }
}
