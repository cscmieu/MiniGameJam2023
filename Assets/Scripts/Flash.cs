using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private float distance = 20f;
    [SerializeField] private float angle = 23f;
    [SerializeField] private float activeperiod = 2f;
    [SerializeField] private Light flashlight;
    private float elapsedTime;
    private float flashTime;
    private void Start()
    {
        flashlight.intensity = 0;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if ((elapsedTime >=  activeperiod) && (flashlight.intensity == 0))
        {
            elapsedTime = 0;
            Detect();
        }
        else if ((elapsedTime >= 0.5) && (flashlight.intensity == 1))
        {
            flashlight.intensity = 0;
        }
    }
    private void Detect()
    {
        flashlight.intensity = 1;
        bool playertouched = false;
        float x = Mathf.Cos(transform.rotation.z - angle / 2);
        float y = Mathf.Sin(transform.rotation.z - angle / 2);
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        for (int i = 1; i <= 9; i += 1)
        {
            playertouched = (playertouched || (Physics2D.Raycast(origin, new Vector2(x, y), distance, LayerMask.GetMask("Player"))));
            x = Mathf.Cos(transform.rotation.z - (4 - i) * angle / 2);
            y = Mathf.Sin(transform.rotation.z - (4 - i) * angle / 2);
        }
        if (playertouched)
        {
            
            //PlayerMovement.Stun();
        }
    }
}
