using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSway : MonoBehaviour 
{
    Color color;

    void Start () 
	{
        color = GetComponent<Text>().color;
    }

    void Update () 
	{
        if (color.r <= 0f)
        {
            StartCoroutine("AddColor");
            StopCoroutine("RemoveColor");
        }

        if (color.r >= 1f)
        {
            StartCoroutine("RemoveColor");
            StopCoroutine("AddColor");
        }
    }

    IEnumerator AddColor()
    {
        for (float i = 0f; i < 1.5f ; i += 0.1f)
        {
            color.r = i;
            GetComponent<Text>().color = color;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator RemoveColor()
    {
        for (float i = 1f; i > -1f; i -= 0.1f)
        {
            color.r = i;
            GetComponent<Text>().color = color;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
