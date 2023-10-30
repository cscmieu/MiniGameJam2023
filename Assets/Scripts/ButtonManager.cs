
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public GameObject creditsPanel;
    [SerializeField] public GameObject menuPanel;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void StartGameEasy()
    {
        SceneManager.LoadScene(1);
        ScoreManager.EasyMode = 2;
    }
    
    public void StartGameHard()
    {
        SceneManager.LoadScene(2);
        ScoreManager.EasyMode = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void ExitCredits()
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