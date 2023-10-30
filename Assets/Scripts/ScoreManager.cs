using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int   Score;
    public static float Time;
    public static int   Malus;
    private       float _startingTime;
    public  static  int  EasyMode;

    private void Awake()
    {
        Score = 0;
        Time = 0;
        Malus = 0;
    }

    private void OnEnable()
    {
        _startingTime = UnityEngine.Time.time;
    }

    public static void UpdateScore(int i)
    {
        Score += i/EasyMode;
    }

    private void Update()
    {
        Time = Mathf.Floor(UnityEngine.Time.time - _startingTime);
    }
}
