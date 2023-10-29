using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void ReturnToMenu()
    {
        AudioManager.Instance.StopMusic("MainMusic");
        AudioManager.Instance.StopMusic("Descent");
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
