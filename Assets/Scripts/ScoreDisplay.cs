using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public static            ScoreDisplay  Instance;
    [SerializeField] private TMP_Text      scoreText;
    private                  RectTransform _rt;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        scoreText.text = "Score : 0";
        _rt            = scoreText.GetComponent<RectTransform>();
    }

    public void ScoreUp()
    {
        scoreText.text = "Score : " + ScoreManager.Score;
        StartCoroutine(ScoreCoroutine());
    }

    private IEnumerator ScoreCoroutine()
    {
        float elapsedTime = 0;
        var startScale = new Vector3(1f,1f,1f);
        var targetScale = new Vector3(1.5f,1.5f,1.5f);

        var timeTakes = .2f;
        while (elapsedTime < timeTakes)
        {
            _rt.localScale = Vector3.Lerp(scoreText.gameObject.transform.transform.localScale, targetScale, (elapsedTime / timeTakes));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        timeTakes = .4f;
        elapsedTime = 0;
        while (elapsedTime < timeTakes)
        {
            _rt.localScale = Vector3.Lerp(scoreText.gameObject.transform.transform.localScale, startScale, (elapsedTime / timeTakes));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
