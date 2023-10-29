using System.Collections;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class KillZone : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshPro score;
    [SerializeField] private TextMeshPro time;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            AudioManager.Instance.PlaySFX("Death");
            pauseMenu.SetActive(false);
            score.gameObject.SetActive(false);
            time.gameObject.SetActive(false);
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
