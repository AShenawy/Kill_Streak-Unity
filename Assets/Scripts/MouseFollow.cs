using UnityEngine;
using System.Collections;

public class MouseFollow : MonoBehaviour
{

    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
