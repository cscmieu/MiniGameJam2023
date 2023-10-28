using UnityEngine;

public class StalactiteSim : MonoBehaviour
{
    [SerializeField] private float groundDistance = 20f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float maxspeed = 15f;
    [SerializeField] private float totalAdditionalFallLength = 1f;
    private float _currentAdditionalFallLength;
    private float _speed;
    private bool _shouldFall;
    private bool _isFalling;
    private bool _hasFallen;

    private void CheckPlayerPresence()
    {
        RaycastHit2D hit;
        Vector2 origin = transform.position;
        hit = Physics2D.Raycast(origin, Vector2.down, groundDistance);
        if (hit.collider.CompareTag("Player"))
        {
            _shouldFall = true;
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    private void Update()
    {
        if (_shouldFall)
        {
            _isFalling = true;
        }

        if (!_isFalling)
        {
            CheckPlayerPresence();
            return;
        }

        if (_speed < maxspeed)
        {
            _speed += acceleration * Time.deltaTime;
        }
        else
        {
            _speed = maxspeed;
        }

        if (_currentAdditionalFallLength < totalAdditionalFallLength)
        {
            transform.Translate(Vector3.down *
                                (_speed * Time
                                    .deltaTime)); //stalactite falls only if it hasn't benn through the ground by over totalAdditionalFallLength
        }
        else
        {
            GetComponent<EdgeCollider2D>().enabled = true;
            GetComponent<PolygonCollider2D>().enabled = false;
        }

        if (_hasFallen)
        {
            _currentAdditionalFallLength +=
                _speed * Time.deltaTime; //stalactite is in the ground, so we count the additional fall length
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EffectManager.KnockBackTriggered = true;
            bool hitToTheRight = other.transform.position.x > transform.position.x ? true : false;
            EffectManager.KnockBackToTheRight = hitToTheRight;
        }
        else if (other.CompareTag("Ground"))
        {
            _hasFallen = true;
        }
    }
}