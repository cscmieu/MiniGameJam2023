using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = "Score : 0";
    }

    private void Update()
    {
        scoreText.text = "Score : " + ScoreManager.score;
    }
}
