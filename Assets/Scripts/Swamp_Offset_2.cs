using System;
using UnityEngine;

public class Swamp_Offset_2 : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start()
    {
        animator.SetBool("ShouldOffsetBy2", true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EffectManager.SlownessTriggered = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EffectManager.SlownessTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EffectManager.SlownessTriggered = false;
        }
    }
}