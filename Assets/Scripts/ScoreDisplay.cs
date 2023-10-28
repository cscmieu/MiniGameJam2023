using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoretext;
    void Start()
    {
        scoretext.text = "Score : 0";
    }

    void Update()
    {
        scoretext.text = "Score : " + ScoreManager.score;
    }
}
