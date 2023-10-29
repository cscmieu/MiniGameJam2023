using UnityEngine;

public class AngryBlock : MonoBehaviour
{
    private                  float         _animationTime;
    private                  bool          _stop = true;
    private                  Rigidbody2D   _rb;
    public                   Animator      animator;
    [SerializeField] private BoxCollider2D detection;
    private static readonly  int           destroyed = Animator.StringToHash("destroyed");

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            EffectManager.KnockBackTriggered = true;
            bool hitToTheRight = other.transform.position.x > transform.position.x;
            EffectManager.KnockBackToTheRight = hitToTheRight;
        }
        _stop = true;
        animator.SetBool(destroyed, true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        detection.enabled = false;
    }

    private void Update()
    {
        if (animator.GetBool(destroyed))
        {
            _animationTime += Time.deltaTime;
            if (_animationTime >= 0.2f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            _stop = (!(detection.IsTouchingLayers(LayerMask.GetMask("Player"))) && _stop);
            if (!_stop)
            {
                var right    = gameObject.transform.right;
                var velocity = _rb.velocity;
                var v =
                    new Vector2(-0.1f * right.x + velocity.x,
                        -0.1f * right.y + velocity.y);
                velocity     = v;
                _rb.velocity = velocity;
            }
        }
    }
}
