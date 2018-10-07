using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour 
{
    [SerializeField]
    float rotationSpeed = 50f;

    void Update () 
	{
        // calculate displacement between mouse and weapon positions
        Vector2 displacement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // calculate the angle/bearing between mouse and weapon positions and convert to Degrees
        // use Atan2 instead of Atan to allow full rotation in 360 degrees
        float angle = Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;

        // convert angle into rotational value about axis of rotation
        Quaternion angularRotation = Quaternion.AngleAxis(angle, Vector3.back);

        // make weapon rotate and smooth out rotation by rotationSpeed
        transform.rotation = Quaternion.Lerp(transform.rotation, angularRotation, rotationSpeed * Time.deltaTime);
    }
    
}
