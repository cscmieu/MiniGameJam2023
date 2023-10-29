using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        Cursor.visible = true;
        AudioManager.Instance.SetMusicVolume(.2f);
        AudioManager.Instance.PlaySFX("Win");
        scoreTresors.text = scoreTresors.text + ScoreManager.score;
        int timeScore = (int) (1 / ScoreManager.time * 5000);
        scoreTime.text = scoreTime.text + timeScore;
        scoreMalus.text = scoreMalus.text + ScoreManager.malus;
        scoreTotal.text = scoreTotal.text + (ScoreManager.score + timeScore - ScoreManager.malus);

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
