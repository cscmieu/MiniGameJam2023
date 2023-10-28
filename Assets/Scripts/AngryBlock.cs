using UnityEngine;

public class AngryBlock : MonoBehaviour
{
    private float animationtime;
    private bool stop = true;
    public Animator animator;
    [SerializeField] private BoxCollider2D detection;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            EffectManager.KnockBackTriggered = true;
        }
        stop = true;
        animator.SetBool("destroyed", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        detection.enabled = false;
    }

    private void Update()
    {
        if (animator.GetBool("destroyed"))
        {
            animationtime += Time.deltaTime;
            if (animationtime >= 0.2f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            stop = (!(detection.IsTouchingLayers(LayerMask.GetMask("Player"))) && stop);
            if (!stop)
            {
                Vector2 veloc =
                    new Vector2(-0.1f * gameObject.transform.right.x + GetComponent<Rigidbody2D>().velocity.x,
                        -0.1f * gameObject.transform.right.y + GetComponent<Rigidbody2D>().velocity.y);
                GetComponent<Rigidbody2D>().velocity = veloc;
            }
        }
    }
}
