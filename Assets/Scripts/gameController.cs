using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    private GameObject begin, movement, spray, kiss;

    public playerController player;
    private paintTag tagScript;
    public setPaint paint;
    public ai aiController;
    private Animator panim, aanim;
    public Animator finalTagAnimator;

    public GameObject dark;
    public GameObject black;
    public AudioSource source;
    public AudioClip close;

    bool quietSource;

	void Start ()
    {
        begin = transform.GetChild(0).gameObject;
        movement = transform.GetChild(1).gameObject;
        spray = transform.GetChild(2).gameObject;
        kiss = transform.GetChild(3).gameObject;

        tagScript = player.gameObject.GetComponent<paintTag>();

        begin.SetActive(true);
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && begin.activeSelf)
            StartCoroutine(StartGame());

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && movement.activeSelf)
            StartCoroutine(InstructPaint());

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && spray.activeSelf)
            spray.GetComponent<fadeUI>().setOff = true;

        if (finalTagAnimator != null)
        {
            float d = Vector2.Distance(player.gameObject.transform.position, aiController.gameObject.transform.position);
            if (d < 2)
                kiss.SetActive(true);
            if (kiss.activeSelf && Input.GetKeyDown(KeyCode.F))
            {
                player.gameObject.transform.position = Vector2.MoveTowards(player.gameObject.transform.position, aiController.gameObject.transform.position, Time.deltaTime * aiController.speed);
                player.enabled = false;
                aiController.enabled = false;
                if (d < 0.7f)
                    StartCoroutine(EndGame());
            }
        }

        if (quietSource)
        {
            if (source.pitch > 0.1)
                source.pitch -= Time.deltaTime;
            if (source.volume < 1)
                source.volume += Time.deltaTime;
        }
    }

    private IEnumerator StartGame()
    {
        begin.SetActive(false);
        player.enabled = true;
        tagScript.enabled = true;
        paint.enabled = true;
        yield return new WaitForSeconds(2);
        aiController.enabled = true;
        movement.SetActive(true);
    }

    private IEnumerator InstructPaint()
    {
        movement.GetComponent<fadeUI>().setOff = true;
        yield return new WaitForSeconds(2);
        spray.SetActive(true);
    }

    private IEnumerator EndGame()
    {
        panim.SetTrigger("kiss");
        aanim.SetTrigger("kiss");
        yield return new WaitForSeconds(3);
        aanim.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        quietSource = true;
        finalTagAnimator.SetTrigger("not enough");
        yield return new WaitForSeconds(0.5f);
        panim.SetTrigger("end");
        dark.SetActive(true);
        yield return new WaitForSeconds(5);
        black.SetActive(true);
        quietSource = false;
        source.pitch = 1;
        source.volume = 0.5f;
        source.clip = close;
        source.loop = false;
        source.Play();
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
