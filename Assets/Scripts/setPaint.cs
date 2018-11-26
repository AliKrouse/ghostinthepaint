using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPaint : MonoBehaviour
{
    public paintTag pt;
    private GameObject[] colors;
    private GameObject large, small;

    public float speed;
    private bool isSelectable;

    private AudioSource source;
    public AudioClip shake;

    void Start()
    {
        source = pt.gameObject.GetComponent<AudioSource>();

        colors = new GameObject[8];
        for (int i = 0; i < colors.Length; i++)
            colors[i] = transform.GetChild(i).gameObject;

        foreach (GameObject c in colors)
        {
            c.transform.position = Vector2.zero;
            c.transform.GetChild(0).position = Vector2.zero;
            c.transform.GetChild(1).position = Vector2.zero;
            c.SetActive(false);
        }
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(1))
            {
                transform.position = pt.mouse;

                foreach (GameObject c in colors)
                {
                    c.transform.position = transform.position;
                    c.transform.GetChild(0).position = c.transform.position;
                    c.transform.GetChild(1).position = c.transform.position;
                    large = null; small = null;
                    c.SetActive(true);
                }

                StartCoroutine(makeSelectable());

                source.clip = shake;
                source.Play();
            }

            if (Input.GetMouseButton(1))
            {
                colors[0].transform.localPosition = Vector2.MoveTowards(colors[0].transform.localPosition, new Vector2(0, 01), Time.deltaTime * speed);
                colors[1].transform.localPosition = Vector2.MoveTowards(colors[1].transform.localPosition, new Vector2(0.7f, 0.7f), Time.deltaTime * speed);
                colors[2].transform.localPosition = Vector2.MoveTowards(colors[2].transform.localPosition, new Vector2(1, 0), Time.deltaTime * speed);
                colors[3].transform.localPosition = Vector2.MoveTowards(colors[3].transform.localPosition, new Vector2(0.7f, -0.7f), Time.deltaTime * speed);
                colors[4].transform.localPosition = Vector2.MoveTowards(colors[4].transform.localPosition, new Vector2(0, -1), Time.deltaTime * speed);
                colors[5].transform.localPosition = Vector2.MoveTowards(colors[5].transform.localPosition, new Vector2(-0.7f, -0.7f), Time.deltaTime * speed);
                colors[6].transform.localPosition = Vector2.MoveTowards(colors[6].transform.localPosition, new Vector2(-1, 0), Time.deltaTime * speed);
                colors[7].transform.localPosition = Vector2.MoveTowards(colors[7].transform.localPosition, new Vector2(-0.7f, 0.7f), Time.deltaTime * speed);

                foreach (GameObject c in colors)
                {
                    float d = Vector2.Distance(c.transform.position, pt.mouse);
                    if (d < 0.25f && isSelectable)
                    {
                        c.GetComponent<SpriteRenderer>().size = new Vector2(0.8f, 0.8f);
                        pt.paintColor = c.GetComponent<SpriteRenderer>().color;
                        large = c.transform.GetChild(0).gameObject;
                        small = c.transform.GetChild(1).gameObject;
                    }
                    else
                    {
                        c.GetComponent<SpriteRenderer>().size = new Vector2(0.7f, 0.7f);
                    }

                    if (c.transform.GetChild(0).gameObject != large)
                        c.transform.GetChild(0).localPosition = Vector2.MoveTowards(c.transform.GetChild(0).localPosition, Vector2.zero, Time.deltaTime * speed);
                    if (c.transform.GetChild(1).gameObject != small)
                        c.transform.GetChild(1).localPosition = Vector2.MoveTowards(c.transform.GetChild(1).localPosition, Vector2.zero, Time.deltaTime * speed);
                }

                if (large != null && small != null)
                {
                    large.transform.localPosition = Vector2.MoveTowards(large.transform.localPosition, new Vector2(-0.3f, 0.75f), Time.deltaTime * speed);
                    small.transform.localPosition = Vector2.MoveTowards(small.transform.localPosition, new Vector2(0.3f, 0.75f), Time.deltaTime * speed);

                    float dl = Vector2.Distance(large.transform.position, pt.mouse);
                    float ds = Vector2.Distance(small.transform.position, pt.mouse);

                    if (dl < 0.2)
                    {
                        pt.paintSize = 0.3f;
                        large.GetComponent<SpriteRenderer>().size = new Vector2(0.6f, 0.6f);
                    }
                    else
                    {
                        large.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, 0.5f);
                    }
                    if (ds < 0.2)
                    {
                        pt.paintSize = 0.1f;
                        small.GetComponent<SpriteRenderer>().size = new Vector2(0.35f, 0.35f);
                    }
                    else
                    {
                        small.GetComponent<SpriteRenderer>().size = new Vector2(0.25f, 0.25f);
                    }
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                foreach (GameObject c in colors)
                {
                    c.SetActive(false);
                }
                isSelectable = false;

                source.Stop();
            }
        }
    }

    private IEnumerator makeSelectable()
    {
        yield return new WaitForSeconds(0.25f);
        isSelectable = true;
    }
}
