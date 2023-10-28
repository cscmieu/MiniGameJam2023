using System;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Bumper : MonoBehaviour
{
    [SerializeField] private float forceBumper = 2; 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Vector2 positionBumper = transform.position;
            Vector2 positionPlayer = other.transform.position;
            Vector2 direction = new Vector2(positionPlayer.x - positionBumper.x, positionPlayer.y - positionBumper.y);
            other.transform.GetComponent<Rigidbody2D>().AddForce(forceBumper * direction, ForceMode2D.Impulse);
        }
    }
}
