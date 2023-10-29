using System.Collections;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private CameraMovement cam;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject time;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            AudioManager.Instance.PlaySFX("Death");
            pauseMenu.SetActive(false);
            score.SetActive(false);
            time.SetActive(false);
            StartCoroutine(GameOverCoroutine());

            cam.isDead = true;
            other.gameObject.GetComponent<PlayerMovement>().inCinematic = true;
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        else
        {
            Destroy(other.gameObject);
        }
        
    }

    private IEnumerator GameOverCoroutine()
    {
        AudioManager.Instance.StopMusic("MainMusic");
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlaySFX("GameOver");
        AudioManager.Instance.PlayMusic("Descent");
        AudioManager.Instance.SetMusicVolume(.2f);
        gameOver.SetActive(true); 
        Cursor.visible = true;
    }
    
}
