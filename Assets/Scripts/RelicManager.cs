using UnityEngine;

public class RelicManager : MonoBehaviour
{
    [SerializeField] Animator relicAnimator;
    [SerializeField] private GameObject managerScore;
    [SerializeField] private TileDecayManager tileDecayManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6) return;
        relicAnimator.SetBool("triggerBreak", true);
    }

    private void OnDestroy()
    {
        tileDecayManager.gameObject.SetActive(true);
        AudioManager.Instance.PlayMusic("MainMusic", true);
        managerScore.SetActive(true);
    }
}
