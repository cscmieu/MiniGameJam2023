using UnityEngine;

public class TreasureTrigger : MonoBehaviour
{

    private Vector3 _velocity;
    [SerializeField] private float time;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6) return;
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, collision.gameObject.transform.position, ref _velocity, time);
    }
}
