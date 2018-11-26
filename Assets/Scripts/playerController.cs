using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 up, down, left, right;
    public float speed;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            up = new Vector2(0, speed);
        }
        else
        {
            up = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.S))
        {
            down = new Vector2(0, -speed);
        }
        else
        {
            down = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.A))
        {
            left = new Vector2(-speed, 0);
        }
        else
        {
            left = Vector2.zero;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = new Vector2(speed, 0);
        }
        else
        {
            right = Vector2.zero;
        }

        rb.velocity = up + down + left + right;

        if (Input.GetKeyDown(KeyCode.F))
        {

        }
	}
}
