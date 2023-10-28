using UnityEngine;

public class SwampEffect : MonoBehaviour
{
    [SerializeField] private Animator animator;
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
