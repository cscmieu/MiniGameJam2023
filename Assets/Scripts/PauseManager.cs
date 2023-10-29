using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    [SerializeField] public Button pauseButton;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public Button resumeButton;
    [SerializeField] public Button homeButton;
    private void Start()
    {
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        homeButton.onClick.AddListener(Home);
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    private void Home()
    {
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
