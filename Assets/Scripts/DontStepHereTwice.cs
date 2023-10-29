using UnityEngine;

public class DontStepHereTwice : MonoBehaviour
{
    [SerializeField] private float delayBeforeActivation = 0.2f;
    private float _elapsedTime;
    private bool _isTriggered;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            _isTriggered = true;
        }
    }
    private void Update()
    {
        if (_isTriggered)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > delayBeforeActivation)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
