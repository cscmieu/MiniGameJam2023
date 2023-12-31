using UnityEngine;

public class Collect : MonoBehaviour
{
    public int value;
    [SerializeField] private string vfx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6) return; // si on touche le joueur
        AudioManager.Instance.PlaySFX(vfx);
        ScoreManager.UpdateScore(value);
        ScoreDisplay.Instance.ScoreUp();
        Destroy(gameObject.transform.parent.gameObject);
    }
}
