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

    public TextMeshProUGUI height;
    public TextMeshProUGUI weight;
    public TextMeshProUGUI age;

    public int height_num;
    public int weight_num;
    public int age_num;


    public int state;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;

        height_num = Int32.Parse(height.text);
        weight_num = Int32.Parse(weight.text);
        age_num = Int32.Parse(age.text);

        Height.SetActive(true);
        Weight.SetActive(false);
        Age.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSetUp()
    {
        if (state == 1)
        {
            Height.SetActive(false);
            Weight.SetActive(true);
            Age.SetActive(false);
        }
        else
        {
            Height.SetActive(false);
            Weight.SetActive(false);
            Age.SetActive(true);
        }
    }

    public void Increase()
    {
        if (state == 0)
        {
            height_num++;
            height.text = height_num.ToString();
        }
        else if (state == 1)
        {
            weight_num++;
            weight.text = weight_num.ToString();
        }
        else
        {
            age_num++;
            age.text = age_num.ToString();
        }
    }

    public void Decrease()
    {
        if (state == 0)
        {
            height_num--;
            height.text = height_num.ToString();
        }
        else if (state == 1)
        {
            weight_num--;
            weight.text = weight_num.ToString();
        }
        else
        {
            age_num--;
            age.text = age_num.ToString();
        }

    }
}
