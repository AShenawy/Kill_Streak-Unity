using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour 
{
    public bool isDestroyable = true;
    public float timeOut = 2f;

    private void Update()
    {
        if(isDestroyable)
        Invoke("DestroyObject", timeOut);
    }

    void DestroyObject()
    {
        GetComponent<Animator>().SetTrigger("Destroy");
        Destroy(gameObject, 1f);
    }


}
