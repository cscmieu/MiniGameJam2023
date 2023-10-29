
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public Button startButton;
    [SerializeField] public Button quitButton;
    [SerializeField] public Button creditsButton;
    [SerializeField] public GameObject creditsPanel;
    [SerializeField] public GameObject menuPanel;
    [SerializeField] public Button exitCreditsButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        creditsButton.onClick.AddListener(OpenCredits);
        exitCreditsButton.onClick.AddListener(ExitCredits);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OpenCredits()
    {
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    private void ExitCredits()
    {
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
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