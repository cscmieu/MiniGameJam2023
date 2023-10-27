using UnityEngine;

public class Flamme_throw : MonoBehaviour
{
    [SerializeField] private double activeTime = 2f;
    [SerializeField] private double disabledTime = 2f;
    private float elapsedTime;
    private bool isActive = false;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if ((elapsedTime >= activeTime && isActive) || (elapsedTime >= disabledTime && !isActive))
        {
            elapsedTime = 0;
            toggleFlame();
        }
    }

    private void toggleFlame()
    {
        if (isActive)
        {
            GetComponent<BoxCollider>().enabled = false;
            isActive = false;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
            isActive = true;
        }
    }
}