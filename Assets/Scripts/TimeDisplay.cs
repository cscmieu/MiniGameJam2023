using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text timetext;
    void Start()
    {
        timetext.text = "Time : 0";
    }

    void Update()
    {
        timetext.text = "Time : " + ScoreManager.time;
    }
}
