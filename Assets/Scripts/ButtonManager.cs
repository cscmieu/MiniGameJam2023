
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

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        creditsButton.onClick.AddListener(OpenCredits);
        exitCreditsButton.onClick.AddListener(ExitCredits);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void OpenCredits()
    {
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    void ExitCredits()
    {
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}