using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeUI : MonoBehaviour
{
    public bool setOff;
    public float speed;

    public bool parent;
    public Image[] children;
    
	void Start ()
    {
        if (parent)
        {
            children = new Image[4];
            for (int i = 0; i < 4; i++)
                children[i] = transform.GetChild(i).GetComponent<Image>();
        }
	}
	
	void Update ()
    {
        if (!setOff)
        {
            if (parent)
            {
                float a = children[0].color.a;
                a += Time.deltaTime * speed;
                foreach (Image i in children)
                    i.color = new Color(1, 1, 1, a);
            }
            else
            {
                float a = GetComponent<Image>().color.a;
                a += Time.deltaTime * speed;
                GetComponent<Image>().color = new Color(1, 1, 1, a);
            }
        }
        else
        {
            if (parent)
            {
                float a = children[0].color.a;
                a -= Time.deltaTime * speed;
                foreach (Image i in children)
                    i.color = new Color(1, 1, 1, a);

                if (a <= 0)
                    this.gameObject.SetActive(false);
            }
            else
            {
                float a = GetComponent<Image>().color.a;
                a -= Time.deltaTime * speed;
                GetComponent<Image>().color = new Color(1, 1, 1, a);

                if (a <= 0)
                    this.gameObject.SetActive(false);
            }
        }
	}
}
