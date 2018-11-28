using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintTag : MonoBehaviour
{
    public GameObject tagBase;
    private LineRenderer tagLine;
    private int currentIndex;

    private float lastCheck;
    public float checkInterval;

    public Vector3 mouse;

    public Color paintColor;
    public float paintSize;

    public float range;
    private AudioSource source;
    public AudioClip spray;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        paintColor = Color.white;
        paintSize = 0.1f;
    }

    void Update()
    {
        mouse = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        float distance = Vector2.Distance(mouse, transform.position);

        if (distance < range)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newTag = Instantiate(tagBase, transform.position, Quaternion.identity);
                tagLine = newTag.GetComponent<LineRenderer>();
                tagLine.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
                tagLine.startColor = paintColor;
                tagLine.endColor = paintColor;
                tagLine.startWidth = paintSize;
                tagLine.endWidth = paintSize;
                tagLine.SetPosition(0, mouse);
                currentIndex = 1;

                source.clip = spray;
                source.Play();
            }

            if (Input.GetMouseButton(0))
            {
                tagLine.SetPosition(currentIndex, mouse);

                if (Time.time - checkInterval > lastCheck)
                {
                    AddNewSegment();
                    lastCheck = Time.time;
                }

                if (!source.isPlaying)
                {
                    source.clip = spray;
                    source.Play();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                tagLine = null;
                currentIndex = 0;

                source.Stop();
            }
        }
        else
        {
            if (source.isPlaying && Input.GetMouseButton(0))
                source.Stop();
        }
    }

    void AddNewSegment()
    {
        tagLine.positionCount += 1;
        currentIndex++;
        tagLine.SetPosition(currentIndex, mouse);
    }
}
