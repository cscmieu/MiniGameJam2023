using UnityEngine;

public class FlameAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float waitDuration = 0.4f;
    private bool switchBool;
    private float elapsedTime;
    public static bool FlammeStart;
    public static bool FlammeStop;

    private void FixedUpdate()
    {
        if (FlammeStart)
        {
            FlammeStart = false;
            animator.SetBool("FlammeStart", true);
            switchBool = true;
        }

        if (FlammeStop)
        {
            FlammeStop = false;
            animator.SetBool(("FlammeStop"),true);
            switchBool = true;
        }

        if (switchBool)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > waitDuration)
            {
                elapsedTime = 0;
                switchBool = false;
                animator.SetBool("FlammeStart", false);
                animator.SetBool(("FlammeStop"),false);
            }
        }
    }
}
