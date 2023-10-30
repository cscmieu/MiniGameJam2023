using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static            GameMenu   Instance;
    [SerializeField] public  GameObject pauseMenu;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject time;
    
    private PlayerMovement _player;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Cursor.visible = false;
        _player = transform.parent.gameObject.GetComponent<PlayerMovement>();
    }

    public void PauseGame()
    {
        Cursor.visible = true;
        _player.inCinematic = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        _player.inCinematic = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void End()
    {
        time.SetActive(false);
        score.SetActive(false);
    }
    public void ReturnToMenu()
    {
        AudioManager.Instance.StopMusic("MainMusic");
        AudioManager.Instance.StopMusic("Descent");
        AudioManager.Instance.SetMusicVolume(.5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Hover()
    {
        AudioManager.Instance.PlaySFX("ButtonHover");
    }
    
    public void Click()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
}
