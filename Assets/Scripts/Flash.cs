using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private float distance = 20f;
    [SerializeField] private float angle = 23f;
    [SerializeField] private float period = 23f;
    private float elapsedTime;
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >=  period)
        {
            elapsedTime = 0;
            Detect();
        }
    }
    private void Detect()
    {
        bool playertouched = false;
        float x = Mathf.Cos(transform.rotation.z - angle / 2);
        float y = Mathf.Sin(transform.rotation.z - angle / 2);
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        for (float i = 1; 9 >= i; i += 1)
        {
            playertouched = (playertouched || (Physics2D.Raycast(origin, new Vector2(x, y), distance, LayerMask.GetMask("Player"))));
            x = Mathf.Cos(transform.rotation.z - (4 - i) * angle / 2);
            y = Mathf.Sin(transform.rotation.z - (4 - i) * angle / 2);
        }
        if (playertouched)
        {
            Debug.LogAssertion("touche");
            //PlayerMovement.Stun();
        }
    }
}
