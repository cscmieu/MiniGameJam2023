using UnityEngine;

public class BreakingPlat : MonoBehaviour
{
    [SerializeField] private float    breakingDuration = 1f;
    [SerializeField] private Animator animator;
    private                  float    _elapsedTime;
    private                  bool     _shouldBreak;
    private static readonly  int      playerStepped = Animator.StringToHash("PlayerStepped");

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
        if (other.gameObject.layer == 6 && !_shouldBreak)
        {
            _shouldBreak = true;
            animator.SetBool(playerStepped, true);
            breakingDuration += _elapsedTime;
        }
    }
}
