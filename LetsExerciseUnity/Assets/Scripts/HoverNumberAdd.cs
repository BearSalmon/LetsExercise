using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HoverNumberAdd : MonoBehaviour
{
    public GameObject triggerObject; // The GameObject that will trigger the incrementing
    public TextMeshProUGUI numberText;
    public int number;
    private Coroutine incrementCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        number = Int32.Parse(numberText.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == triggerObject)
        {
            // Start incrementing the number when the trigger GameObject enters the button's trigger zone
            incrementCoroutine = StartCoroutine(IncrementNumber());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == triggerObject)
        {
            // Stop incrementing the number when the trigger GameObject exits the button's trigger zone
            if (incrementCoroutine != null)
            {
                StopCoroutine(incrementCoroutine);
            }
        }
    }

    private IEnumerator IncrementNumber()
    {
        while (true)
        {
            // Increment the number and update the text
            number++;
            numberText.text = number.ToString();

            // Wait for one second before incrementing again
            yield return new WaitForSeconds(1f);
        }
    }
}
