using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] public int value = 0;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            ScoreManager.Instance.UpdateScore(value);
            Destroy(gameObject);
        }
    }
}
