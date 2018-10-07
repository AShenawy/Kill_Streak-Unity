using UnityEngine;
using System.Collections;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] int lifeValue = 1;
    [SerializeField] bool isCollected = false;

    private void Update()
    {
        if (!isCollected)
        {   isCollected = true;
            GameManager.gm.GainLife(lifeValue);
        }
    }

}
