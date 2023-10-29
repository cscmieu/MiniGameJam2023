using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int Score;
    public static float Time;
    public static int Malus;
    private float _startingTime;
    public static ScoreManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        Score = 0;
        Time = 0;
        Malus = 0;
    }

    private void OnEnable()
    {
        _startingTime = UnityEngine.Time.time;
    }

    public void UpdateScore(int i)
    {
        Score += i;
    }
    
    void Update()
    {
        Time = Mathf.Floor(UnityEngine.Time.time - _startingTime);
    }
}
