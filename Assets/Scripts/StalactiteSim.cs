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
        RaycastHit hit;
        Vector3 origin = transform.position;
        _shouldFall = (Physics.Raycast(origin, Vector3.down, out hit, groundDistance) &&
                       hit.transform.CompareTag("Player"));
    }

    private void Update()
    {
        CheckPlayerPresence();
        if (_shouldFall)
        {
            _isFalling = true;
        }

        if (_isFalling)
        {
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
                transform.Translate(Vector3.down * (_speed * Time.deltaTime));
            }

            if (_hasFallen)
            {
                _currentAdditionalFallLength += _speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player was knocked Back");
            //collider.knockback();
        }
        else if (other.CompareTag("Ground"))
        {
            _hasFallen = true;
        }
    }
}