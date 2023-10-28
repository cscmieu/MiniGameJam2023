using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] 
    private PlayerMovement target;
    [SerializeField] 
    private GameObject rope;
    [SerializeField] 
    private float xMin = -1, xMax = 12, speed = .5f;

    private Vector3 velocity;
    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.x > rope.transform.position.x - 3 &&
            target.transform.position.x < rope.transform.position.x + 3)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(rope.transform.position.x, target.transform.position.y, 0), ref velocity, speed);
        }
        else if (target.cameraTarget.transform.position.x > xMin && 
                 target.cameraTarget.transform.position.x < xMax)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.cameraTarget.transform.position, ref velocity, speed);
        }
        else if (target.cameraTarget.transform.position.x < xMin)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMin, target.transform.position.y, 0), ref velocity, speed);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMax, target.transform.position.y, 0), ref velocity, speed);
        }
    }
}
