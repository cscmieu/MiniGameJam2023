using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public float time;
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        score = 0;
        time = 0;
    }

    public void UpdateScore(int i)
    {
        score += i;
    }
    
    void Update()
    {
        time = Mathf.Floor(Time.time);
    }
}
