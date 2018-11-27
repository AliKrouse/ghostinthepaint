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
    public int[] tagLayers;
    private int tagIndex;
    public List<Sprite> tags;
    public GameObject tagPrefab;
    public GameObject lastTag;

    bool coroutineIsRunning;

    public gameController gc;
    
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
            Vector2 direction = (transform.position - waypoints[index].position).normalized;

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
            
            if (direction.x < 0)
                sr.flipX = false;
            else
                sr.flipX = true;

            if (Mathf.Abs(direction.x) > 0.25)
            {
                anim.SetBool("lr", true);
                anim.SetBool("front", false);
                anim.SetBool("back", false);
            }
            else
            {
                if (direction.y > 0)
                {
                    anim.SetBool("back", false);
                    anim.SetBool("front", true);
                    anim.SetBool("lr", false);
                }
                if (direction.y < 0)
                {
                    anim.SetBool("front", false);
                    anim.SetBool("back", true);
                    anim.SetBool("lr", false);
                }
            }
        }
        if (coroutineIsRunning)
        {
            anim.SetBool("moving", false);
        }
        else
        {
            anim.SetBool("moving", true);
        }
        if (index == waypoints.Length && !coroutineIsRunning)
        {
            StartCoroutine(LastTag());
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
        t.GetComponent<SpriteRenderer>().sortingOrder = tagLayers[tagIndex];
        tags.RemoveAt(randomTag);
        yield return new WaitForSeconds(0.25f);
        tagIndex++;
        index++;
        coroutineIsRunning = false;
    }

    private IEnumerator LastTag()
    {
        coroutineIsRunning = true;
        float think = Random.Range(5f, 10f);
        yield return new WaitForSeconds(think);
        source.Play();
        yield return new WaitForSeconds(pauseTime);
        source.Stop();
        GameObject t = Instantiate(lastTag);
        gc.finalTagAnimator = t.GetComponent<Animator>();
    }
}
