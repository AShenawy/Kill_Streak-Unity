using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour 
{
    void Update () 
	{
        StartCoroutine("UpScale");
	}

    IEnumerator UpScale()
    {
        for (float i=0f; i<1.1f ; i+=0.07f)
        {
            Vector3 scale = transform.localScale;

            scale.x = i;
            scale.y = i;

            transform.localScale = scale;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
