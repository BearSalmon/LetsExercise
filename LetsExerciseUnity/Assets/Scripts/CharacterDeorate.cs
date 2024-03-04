using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterDeorate : MonoBehaviour
{
    public int state = 0;
    public GameObject palette_hair;
    public GameObject palette_body;
    public GameObject palette_cloth;

    public RawImage hair;
    public RawImage body;
    public RawImage cloth;

    // Start is called before the first frame update
    void Start()
    {
        hair = GameObject.Find("hair_girl").GetComponent<RawImage>();
        body = GameObject.Find("body_girl").GetComponent<RawImage>();
        cloth = GameObject.Find("cloth_girl").GetComponent<RawImage>();

    }

    public void changeState()
    {
        if (state == 1)
        {
            palette_hair.SetActive(false);
            palette_body.SetActive(true);
        }
        else if (state == 2)
        {
            palette_body.SetActive(false);
            palette_cloth.SetActive(true);
        }
    }

    public void changeColor(Color color)
    {
        if (state == 0)
        {
            hair.color = color;
        }
        else if (state == 1)
        {
            body.color = color;
        }
        else
        {
            cloth.color = color;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
