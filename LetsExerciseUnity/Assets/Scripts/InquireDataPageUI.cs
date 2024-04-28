using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InquireDataPageUI : MonoBehaviour
{

    public GameObject Height;
    public GameObject Weight;
    public GameObject Age;
    public GameObject Ask;

    public TextMeshProUGUI height;
    public TextMeshProUGUI weight;
    public TextMeshProUGUI age;

    public int height_num;
    public int weight_num;
    public int age_num;

    ButtonEvent buttonEvent;

    public int state;

    // Start is called before the first frame update
    void Start()
    {
        buttonEvent = GameObject.Find("WholeManager").GetComponent<ButtonEvent>();
        if (buttonEvent.isAddingWeight == false)
        {
            state = 0;
            height_num = Int32.Parse(height.text);
            weight_num = Int32.Parse(weight.text);
            age_num = Int32.Parse(age.text);

            Ask.SetActive(true);
            Height.SetActive(false);
            Weight.SetActive(false);
            Age.SetActive(false);


        }
        else
        {
            state = 2;
            weight_num = Int32.Parse(weight.text);
            Ask.SetActive(false);
            Height.SetActive(false);
            Weight.SetActive(true);
            Age.SetActive(false);
        }

    }

    public void ChangeSetUp()
    {
        
        if (state == 1)
        {
            Ask.SetActive(false);
            Height.SetActive(true);
            Weight.SetActive(false);
            Age.SetActive(false);
        }
        else if (state == 2)
        {
            Height.SetActive(false);
            Weight.SetActive(true);
            Age.SetActive(false);
        }
        else if (state == 3)
        {
            Height.SetActive(false);
            Weight.SetActive(false);
            Age.SetActive(true);
        }
    }

    public void Increase()
    {
        if (state == 1)
        {
            height_num++;
            height.text = height_num.ToString();
        }
        else if (state == 2)
        {
            weight_num++;
            weight.text = weight_num.ToString();
        }
        else if (state == 3)
        {
            age_num++;
            age.text = age_num.ToString();
        }
    }

    public void Decrease()
    {
        if (state == 1)
        {
            height_num--;
            height.text = height_num.ToString();
        }
        else if (state == 2)
        {
            weight_num--;
            weight.text = weight_num.ToString();
        }
        else if (state == 3)
        {
            age_num--;
            age.text = age_num.ToString();
        }

    }
}
