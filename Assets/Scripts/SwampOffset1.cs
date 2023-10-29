using UnityEngine;

public class SwampOffset1 : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly  int      shouldOffsetBy1 = Animator.StringToHash("ShouldOffsetBy1");

    private void Start()
    {
        animator.SetBool(shouldOffsetBy1, true);
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

