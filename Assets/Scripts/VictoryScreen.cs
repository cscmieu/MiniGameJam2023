using System.Collections;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text scoreTresors;
    [SerializeField] 
    private TMP_Text scoreTime;
    [SerializeField] 
    private TMP_Text scoreMalus;
    [SerializeField] 
    private TMP_Text scoreTotal;
    [SerializeField] 
    private GameObject button;
    
    // Start is called before the first frame update
    public void Start()
    {
        Cursor.visible = true;
        AudioManager.Instance.SetMusicVolume(.2f);
        AudioManager.Instance.PlaySFX("Win");
        scoreTresors.text = scoreTresors.text + ScoreManager.Score;
        int timeScore = (int) (1000 - Mathf.Max(Mathf.Sqrt(ScoreManager.Time) * 80, 0));
        scoreTime.text = scoreTime.text + timeScore;
        scoreMalus.text = scoreMalus.text + ScoreManager.Malus;
        scoreTotal.text = scoreTotal.text + (ScoreManager.Score + timeScore - ScoreManager.Malus);

        StartCoroutine(VictoryCoroutine());
    }

    private IEnumerator VictoryCoroutine()
    {
        yield return new WaitForSeconds(1);
        scoreTresors.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        scoreTime.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        scoreMalus.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        scoreTotal.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        button.SetActive(true);
    }

    
}
