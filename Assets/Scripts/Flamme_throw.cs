using System;
/*using UnityEngine;

public class Flamme_throw : MonoBehaviour
{
    [SerializeField] private double activeTime = 2f;
    [SerializeField] private double disabledTime = 2f;
    private float elapsedTime;
    private bool isActive = false;

    private void Start()
    {
        FlameAnim.FlammeStart = true;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if ((elapsedTime >= activeTime && isActive) || (elapsedTime >= disabledTime && !isActive))
        {
            elapsedTime = 0;
            toggleFlame();
        }

        if ((elapsedTime >= disabledTime - 0.2f) && !isActive)
        {
            FlameAnim.FlammeStart = true;
        }
    }

    private void toggleFlame()
    {
        if (isActive)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            isActive = false;
            FlameAnim.FlammeStop = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
            isActive = true;
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
    }
}*/