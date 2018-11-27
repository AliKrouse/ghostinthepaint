using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organizeMovingRenderer : organizeRenderer
{
    private Transform foot;

    protected override void Start()
    {
        foot = transform.GetChild(0);
        base.Start();
    }

    void Update ()
    {
        Organize();
	}

    protected override void Organize()
    {
        sr.sortingOrder = Mathf.RoundToInt(foot.position.y * -100);
    }
}
