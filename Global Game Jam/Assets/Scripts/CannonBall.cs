using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    Vector3 endPos;
    public Rigidbody2D body;

    public float gravity = 0;
    public void InitCannon(Vector3 endPos, int seconds){
        this.endPos = endPos;
        body.velocity = (endPos - transform.position) / seconds;
    }

}
