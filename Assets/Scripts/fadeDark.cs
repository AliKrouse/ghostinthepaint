using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeDark : MonoBehaviour
{
    public float speed;
    private SpriteRenderer sr;
    
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        float a = sr.color.a;
        a += Time.deltaTime * speed;
        sr.color = new Color(1, 1, 1, a);
        if (a >= 1)
        {
            if (transform.localScale.x > 8)
            {
                float s = transform.localScale.x;
                s -= Time.deltaTime * speed;
                transform.localScale = new Vector3(s, s, 1);
            }
        }
	}
}
