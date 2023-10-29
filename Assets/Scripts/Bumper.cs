using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private float      forceBumper = 2f; 
    public                   Animator   animator;
    private                  GameObject _player;
    private                  float      _animationTime;
    private static readonly  int        isBumping = Animator.StringToHash("IsBumping");

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != 6) return;
        
        EffectManager.BumpTriggered                           = true;
        _player                                               = other.gameObject;
        _player.GetComponent<PlayerMovement>()._inputDisabled = true;
        animator.SetBool(isBumping, true);
        Vector2 positionBumper = gameObject.transform.position;
        Vector2 positionPlayer = other.transform.position;
        var     direction      = new Vector2(positionPlayer.x - positionBumper.x, positionPlayer.y - positionBumper.y).normalized;
        other.transform.GetComponent<Rigidbody2D>().AddForce(forceBumper * direction, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (!animator.GetBool(isBumping)) return;
        
        _animationTime += Time.deltaTime;
        
        if (!(_animationTime >= 0.2f)) return;
        
        animator.SetBool(isBumping, false);
        _animationTime = 0f;
    }
}
