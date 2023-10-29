using System;
using UnityEngine;

public class FlameAnim : MonoBehaviour
{
    [SerializeField] private Animator      animator;
    [SerializeField] private double        activeTime   = 2f;
    [SerializeField] private double        disabledTime = 2f;
    [SerializeField] private double        hitBoxDelay  = 0.2f;
    private                  bool          _inDelay;
    private                  float         _elapsedTime;
    private                  bool          _flammeActive;
    private static readonly  int           flammeStop = Animator.StringToHash("FlammeStop");
    private                  BoxCollider2D _bc;

    private void Start()
    {
        _bc = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    { 
        if (!_flammeActive && !_inDelay)
        {
            animator.SetBool(flammeStop,true);
        }
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= activeTime && _flammeActive)
        {
            _elapsedTime = 0;
            ToggleFlame();
        }
        else if ((_elapsedTime >= disabledTime - hitBoxDelay) && (_elapsedTime < disabledTime) && !_flammeActive)
        {
            animator.SetBool(flammeStop, false);
            _inDelay = true;
        }
        else if ((_elapsedTime >= disabledTime) && !_flammeActive)
        {
            _elapsedTime = 0;
            ToggleFlame();
        }
    }
    private void ToggleFlame()
    {
        if (!_flammeActive)
        {
            _bc.enabled = true;
            _flammeActive = true;
            _inDelay = false;
        }
        else
        {
            _bc.enabled = false;
            _flammeActive = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 6) return;
        
        EffectManager.KnockBackTriggered = true;
        var hitToTheRight = other.transform.position.x > transform.position.x;
        EffectManager.KnockBackToTheRight = hitToTheRight;
    }
}
