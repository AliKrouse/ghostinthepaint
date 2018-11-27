using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private Vector2 up, down, left, right;
    public float speed;

    public GameObject ai;
    private ParticleSystem hearts;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        hearts = GetComponent<ParticleSystem>();
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


        if (rb.velocity != Vector2.zero)
        {
            anim.SetBool("moving", true);

            if (Mathf.Abs(rb.velocity.x) > float.Epsilon)
            {
                anim.SetBool("lr", true);
                anim.SetBool("front", false);
                anim.SetBool("back", false);

                if (rb.velocity.x > float.Epsilon)
                    sr.flipX = false;
                if (rb.velocity.x < -float.Epsilon)
                    sr.flipX = true;
            }
            else if (rb.velocity.y > float.Epsilon)
            {
                anim.SetBool("front", false);
                anim.SetBool("back", true);
                anim.SetBool("lr", false);
            }
            else if (rb.velocity.y < float.Epsilon)
            {
                anim.SetBool("back", false);
                anim.SetBool("front", true);
                anim.SetBool("lr", false);
            }
        }
        else
        {
            anim.SetBool("moving", false);
        }

        float d = Vector2.Distance(transform.position, ai.transform.position);
        if (d <= 3)
        {
            if (!hearts.isPlaying)
                hearts.Play();
        }
        else
        {
            if (hearts.isPlaying)
                hearts.Stop();
        }
    }
}
