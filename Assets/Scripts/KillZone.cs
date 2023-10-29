using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            AudioManager.Instance.PlaySFX("Death");
            gameOver.SetActive(true);    
        }
        
	    Destroy(other.gameObject);
    }
    
    
}
