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
    private TMP_Text scoreTotal;
    // Start is called before the first frame update
    void Start()
    {
        scoreTresors.text = scoreTresors.text + ScoreManager.score;
        int timeScore = (int) (1 / ScoreManager.time * 30000);
        scoreTime.text = scoreTime.text + timeScore;
        scoreTotal.text = scoreTotal.text + (ScoreManager.score + timeScore);
    }

    
}