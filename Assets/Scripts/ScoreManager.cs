using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static float time;
    public static int malus;
    private float startingTime;
    public static ScoreManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        score = 0;
        time = 0;
        malus = 0;
    }

    private void OnEnable()
    {
        startingTime = Time.time;
    }

    public void UpdateScore(int i)
    {
        score += i;
    }
    
    void Update()
    {
        time = Mathf.Floor(Time.time - startingTime);
    }
}
