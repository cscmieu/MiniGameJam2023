using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private float distance = 20f;
    [SerializeField] private float angle = 23f;
    [SerializeField] private float activeperiod = 2f;
    [SerializeField] private Light flashlight;
    [SerializeField] private float intensity = 4f;
    private float elapsedTime;
    private float flashTime;
    private void Start()
    {
        flashlight.intensity = 0;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if ((elapsedTime >= activeperiod) && (flashlight.intensity == 0))
        {
            elapsedTime = 0;
            Detect();
        }
        else if ((elapsedTime >= 0.1) && (flashlight.intensity == intensity))
        {
            flashlight.intensity = 0f;
        }
    }
    private void Detect()
    {
        flashlight.intensity = intensity;
        bool playertouched = false;
        float x = Mathf.Cos(transform.rotation.z - angle / 2);
        float y = Mathf.Sin(transform.rotation.z - angle / 2);
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        for (int i = 1; i <= 9; i += 1)
        {
            playertouched = (playertouched ||
                             ((!Physics2D.Raycast(origin, new Vector2(x, y), distance,
                                 Mathf.Abs(1 - (LayerMask.GetMask("Player")))) && (Physics2D.Raycast(origin,
                                 new Vector2(x, y), distance, LayerMask.GetMask("Player"))))));
            x = Mathf.Cos(transform.rotation.z - (4 - i) * angle / 2);
            y = Mathf.Sin(transform.rotation.z - (4 - i) * angle / 2);
        }
        if (playertouched)
        {
            Debug.LogAssertion("touche");
            EffectManager.StunTriggered = true;
        }
    }
}