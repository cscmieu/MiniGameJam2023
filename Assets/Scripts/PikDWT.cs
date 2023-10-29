using System;
using System.Collections;
using UnityEngine;

public class PikDWT : MonoBehaviour
{
    public Animator animator;

    private static readonly int isActivated = Animator.StringToHash("isActivated");

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        StartCoroutine(Waiting());
        animator.SetBool(isActivated, true);
    }
    
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.01f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 6) return;
        EffectManager.KnockBackTriggered = true;
        var hitToTheRight = other.transform.position.x > transform.position.x;
        EffectManager.KnockBackToTheRight = hitToTheRight;
    }
}
