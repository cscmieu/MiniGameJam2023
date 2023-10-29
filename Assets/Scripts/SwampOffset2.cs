using UnityEngine;

public class SwampOffset2 : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly  int      shouldOffsetBy2 = Animator.StringToHash("ShouldOffsetBy2");

    private void Start()
    {
        animator.SetBool(shouldOffsetBy2, true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            EffectManager.SlownessTriggered = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            EffectManager.SlownessTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            EffectManager.SlownessTriggered = false;
        }
    }
}