using System.Collections;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            AudioManager.Instance.PlaySFX("Death");
            StartCoroutine(GameOverCoroutine());
        }
        
	    Destroy(other.gameObject);
    }

    private IEnumerator GameOverCoroutine()
    {
        AudioManager.Instance.StopMusic("MainMusic");
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlaySFX("GameOver");
        AudioManager.Instance.PlayMusic("Descent");
        AudioManager.Instance.SetMusicVolume(.2f);
        gameOver.SetActive(true); 
    }
    
}
