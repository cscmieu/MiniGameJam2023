using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    public void Start()
    {
        timeText.text = "Time : 0";
    }

    public void Update()
    {
        timeText.text = "Time : " + ScoreManager.Time;
    }
}
