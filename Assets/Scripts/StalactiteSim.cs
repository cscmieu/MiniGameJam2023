using UnityEngine;

public class StalactiteSim : MonoBehaviour
{
    [SerializeField] private float       groundDistance            = 20f;
    [SerializeField] private float       acceleration              = 15f;
    [SerializeField] private float       maxSpeed                  = 15f;
    [SerializeField] private float       totalAdditionalFallLength = 1f;
    private                  Rigidbody2D _rb;
    private                  PolygonCollider2D _pc;
    private                  EdgeCollider2D _ec;
    private                  float       _currentAdditionalFallLength;
    private                  float       _speed;
    private                  bool        _shouldFall;
    private                  bool        _isFalling;
    private                  bool        _hasFallen;

    private void Start()
    {
        _rb                                       = GetComponent<Rigidbody2D>();
        _ec = GetComponent<EdgeCollider2D>();
        _pc = GetComponent<PolygonCollider2D>();
    }

    private void CheckPlayerPresence()
    {
        Vector2      origin = transform.position;
        var hit    = Physics2D.Raycast(origin, Vector2.down, groundDistance);
        if (hit.collider.gameObject.layer != 6) return;
        _shouldFall   = true;
        _rb.simulated = true;
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

        if (_speed < maxSpeed)
        {
            _speed += acceleration * Time.deltaTime;
        }
        else
        {
            _speed = maxSpeed;
        }

        if (_currentAdditionalFallLength < totalAdditionalFallLength)
        {
            transform.Translate(Vector3.down *
                                (_speed * Time
                                    .deltaTime)); //stalactite falls only if it hasn't benn through the ground by over totalAdditionalFallLength
        }
        else
        {
            _ec.enabled = true;
            _pc.enabled = false;
        }

        if (_hasFallen)
        {
            _currentAdditionalFallLength +=
                _speed * Time.deltaTime; //stalactite is in the ground, so we count the additional fall length
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.layer)
        {
            case 6:
            {
                EffectManager.KnockBackTriggered = true;
                var hitToTheRight = other.transform.position.x > transform.position.x;
                EffectManager.KnockBackToTheRight = hitToTheRight;
                break;
            }
            case 0:
                _hasFallen = true;
                break;
        }
    }
}