using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flash : MonoBehaviour
{
    [SerializeField] private float             activePeriod = 2f;
    [SerializeField] private Light2D           flashlight;
    [SerializeField] private float             intensity = 0.5f;
    private                  float             _elapsedTime;
    private                  bool              _activated;
    private                  PolygonCollider2D _pc;
    private void Start()
    {
        flashlight.intensity = 0f;
        _pc                  = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if ((_elapsedTime >= activePeriod) && (flashlight.intensity == 0f))
        {
            _elapsedTime = 0f;
            flashlight.intensity = intensity;
            _activated = true;
        }
        else if ((_elapsedTime >= 0.1f) && (Math.Abs(flashlight.intensity - intensity) < .01f))
        {
            flashlight.intensity = 0f;
            _activated = false;
        }
        if (_pc.IsTouchingLayers(LayerMask.GetMask("Player")) && (_activated))
        {
            EffectManager.StunTriggered = true;
        }
    }
/*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.layer == 6) && (_activated))
        {
            EffectManager.StunTriggered = true;
        }
    }*/
}