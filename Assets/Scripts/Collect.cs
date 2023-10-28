using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] public int value = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6) return; // si on touche le joueur
        ScoreManager.Instance.UpdateScore(value);
        Destroy(gameObject);
    }
}
