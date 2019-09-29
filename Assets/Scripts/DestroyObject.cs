using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour 
{
    [SerializeField] float waitForDestroy = 1f;

	void Update () 
	{
        Destroy(gameObject, waitForDestroy);
    }
}
