using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnQuit : MonoBehaviour 
{

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
