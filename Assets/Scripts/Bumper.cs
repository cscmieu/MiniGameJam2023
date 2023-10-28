using System;
using System.Collections;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Bumper : MonoBehaviour
{
    [SerializeField] private float forceBumper = 2f; 
    public Animator animator;
    private GameObject player;
    private float animationtime;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            EffectManager.BumpTriggered = true;
            player = other.gameObject;
            player.GetComponent<PlayerMovement>()._inputDisabled = true;
            animator.SetBool("IsBumping", true);
            Vector2 positionBumper = gameObject.transform.position;
            Vector2 positionPlayer = other.transform.position;
            Vector2 direction = new Vector2(positionPlayer.x - positionBumper.x, positionPlayer.y - positionBumper.y).normalized;
            other.transform.GetComponent<Rigidbody2D>().AddForce(forceBumper * direction, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if (animator.GetBool("IsBumping"))
        {
            animationtime += Time.deltaTime;
            if (animationtime >= 0.2f)
            {
                animator.SetBool("IsBumping", false);
                animationtime = 0f;
            }
        }
    }
}
