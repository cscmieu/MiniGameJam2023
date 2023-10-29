using UnityEngine;

public class FlameAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private double activeTime = 2f;
    [SerializeField] private double disabledTime = 2f;
    [SerializeField] private double hitBoxDelay = 0.2f;
    private bool inDelay;
    private float elapsedTime;
    private bool FlammeActive;

    private void FixedUpdate()
    { 
        if (!FlammeActive && !inDelay)
        {
            animator.SetBool(("FlammeStop"),true);
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= activeTime && FlammeActive)
        {
            elapsedTime = 0;
            ToggleFlame();
        }
        else if ((elapsedTime >= disabledTime - hitBoxDelay) && (elapsedTime < disabledTime) && !FlammeActive)
        {
            animator.SetBool("FlammeStop", false);
            inDelay = true;
        }
        else if ((elapsedTime >= disabledTime) && !FlammeActive)
        {
            elapsedTime = 0;
            ToggleFlame();
        }
    }
    private void ToggleFlame()
    {
        if (!FlammeActive)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            FlammeActive = true;
            inDelay = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
            FlammeActive = false;
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
}
