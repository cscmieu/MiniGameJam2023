using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] 
    private PlayerMovement target;
    [SerializeField] 
    private GameObject rope;
    [SerializeField] 
    private float xMin = -1, xMax = 12, speed = .5f;

    [SerializeField] 
    private Transform endCinematicTarget;
    
    
    private Vector3 _velocity;
    public bool isInStartCinematic;
    public bool isInEndCinematic;

    private bool _isTargetNull;

    void Update()
    {
        if (target is null) return;
        
        if (isInStartCinematic)
        {
            transform.position = target.transform.position;
            return;
        }

        if (isInEndCinematic)
        {
            transform.position = Vector3.SmoothDamp(transform.position, endCinematicTarget.position, ref _velocity, 3);
            return;
        }

        if (target.transform.position.x > rope.transform.position.x - 3 &&
            target.transform.position.x < rope.transform.position.x + 3)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(rope.transform.position.x, target.transform.position.y, 0), ref _velocity, speed);
        }
        else if (target.cameraTarget.transform.position.x > xMin && 
                 target.cameraTarget.transform.position.x < xMax)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.cameraTarget.transform.position, ref _velocity, speed);
        }
        else if (target.cameraTarget.transform.position.x < xMin)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMin, target.transform.position.y, 0), ref _velocity, speed);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xMax, target.transform.position.y, 0), ref _velocity, speed);
        }
    }
}
