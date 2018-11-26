using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organizeRenderer : MonoBehaviour
{
    protected SpriteRenderer sr;
    
	protected virtual void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
	}

    protected virtual void Organize()
    {
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}
