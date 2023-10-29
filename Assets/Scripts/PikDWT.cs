using System;
using System.Collections;
using UnityEngine;

public class PikDWT : MonoBehaviour
{
    public Animator animator;

    private static readonly int isActivated = Animator.StringToHash("isActivated");

    //private bool activated;
    //private bool stopmove;
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(Waiting());
            animator.SetBool(isActivated, true);
        }
    }
    
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.01f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            EffectManager.KnockBackTriggered = true;
            bool hitToTheRight = other.transform.position.x > transform.position.x ? true : false;
            EffectManager.KnockBackToTheRight = hitToTheRight;
        }
    }
}
