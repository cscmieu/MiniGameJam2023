using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] public int value = 0; 
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        ScoreManager.Instance.UpdateScore(value);
        Destroy(gameObject);
    }
    

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ScoreManager.Instance.UpdateScore(40);
        }
    }*/
}
