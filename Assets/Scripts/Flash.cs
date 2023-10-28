using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flash : MonoBehaviour
{
    [SerializeField] private float activeperiod = 2f;
    [SerializeField] private Light2D flashlight;
    [SerializeField] private float intensity = 0.5f;
    private float _elapsedTime;
    private bool _activated;
    private void Start()
    {
        flashlight.intensity = 0f;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if ((_elapsedTime >= activeperiod) && (flashlight.intensity == 0f))
        {
            _elapsedTime = 0f;
            flashlight.intensity = intensity;
            _activated = true;
        }
        else if ((_elapsedTime >= 0.1f) && (flashlight.intensity == intensity))
        {
            flashlight.intensity = 0f;
            _activated = false;
        }
        if (gameObject.GetComponent<PolygonCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")) && (_activated))
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