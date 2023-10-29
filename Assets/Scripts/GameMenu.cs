using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void ReturnToMenu()
    {
        AudioManager.Instance.StopMusic("MainMusic");
        AudioManager.Instance.StopMusic("Descent");
        AudioManager.Instance.SetMusicVolume(.5f);
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
