using UnityEngine;
using System.Collections;

public class LookAtTarget2D : MonoBehaviour
{

    GameObject target;

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindWithTag("Bullet");
        
        // calculate displacement between mouse and weapon positions
        Vector2 displacement = target.transform.position - transform.position;

        // calculate the angle/bearing between object and target positions and convert to Degrees
        // use Atan2 instead of Atan to allow full rotation in 360 degrees
        float angle = Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg;

        // convert angle into rotational value about axis of rotation
        transform.rotation = Quaternion.AngleAxis(angle +90, Vector3.forward);
    }
}
