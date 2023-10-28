using UnityEngine;

public class AngryBlock : MonoBehaviour
{
    private bool stop = true;
    [SerializeField] private BoxCollider2D detection;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //PlayerMovement.Knockback();
        }
        Debug.LogAssertion("touche");
        stop = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        detection.enabled = false;
    }

    private void Update()
    {
        stop = (!(detection.IsTouchingLayers(LayerMask.GetMask("Player"))) && stop);
        if (!stop)
        {
            Vector2 veloc = new Vector2(0.2f * gameObject.transform.right.x + GetComponent<Rigidbody2D>().velocity.x, 0.2f * gameObject.transform.right.y + GetComponent<Rigidbody2D>().velocity.y);
            GetComponent<Rigidbody2D>().velocity = veloc;
        }
    }
}