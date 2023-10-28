using UnityEngine;

public class BreakingPlat : MonoBehaviour
{
    [SerializeField] private float breakingDuration = 1f;
    [SerializeField] private Animator animator;
    private float _elapsedTime;
    private bool _shouldBreak;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_shouldBreak && _elapsedTime > breakingDuration)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_shouldBreak && other.gameObject.transform.position.y > (transform.position.y + transform.lossyScale.y * gameObject.GetComponent<BoxCollider2D>().size.y)/2f)
        {
            _shouldBreak = true;
            animator.SetBool("PlayerStepped", true);
            breakingDuration += _elapsedTime;
        }
    }
}
