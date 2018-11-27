using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private AudioSource source;

    public Transform[] waypoints;
    private int index;
    public float speed;
    public float pauseTime;
    public Transform[] tagPoints;
    private int tagIndex;
    public List<Sprite> tags;
    public GameObject tagPrefab;

    bool coroutineIsRunning;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
        if (index < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[index].position, Time.deltaTime * speed);
            float distance = Vector2.Distance(transform.position, waypoints[index].position);

            if (distance < float.Epsilon)
            {
                if (waypoints[index].gameObject.CompareTag("tagging point") && !coroutineIsRunning)
                {
                    StartCoroutine(Tag());
                }
                else if (!waypoints[index].gameObject.CompareTag("tagging point"))
                {
                    index++;
                }
            }
        }
	}

    private IEnumerator Tag()
    {
        coroutineIsRunning = true;
        float think = Random.Range(5f, 10f);
        yield return new WaitForSeconds(think);
        source.Play();
        yield return new WaitForSeconds(pauseTime);
        source.Stop();
        GameObject t = Instantiate(tagPrefab, tagPoints[tagIndex].position, tagPoints[tagIndex].rotation);
        int randomTag = Random.Range(0, tags.Count);
        t.GetComponent<SpriteRenderer>().sprite = tags[randomTag];
        tags.RemoveAt(randomTag);
        yield return new WaitForSeconds(0.25f);
        tagIndex++;
        index++;
        coroutineIsRunning = false;
    }
}
