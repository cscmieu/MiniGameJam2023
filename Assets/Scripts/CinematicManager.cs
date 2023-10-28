using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private CameraMovement cam;
    [SerializeField] private float timeToDescend = 5f;
    [SerializeField] private TileDecayManager tileDecayManager;
    private Vector3 velocity;
 
    void Start()
    {
        player.inCinematic = true;
        cam.isInCinematic = true;
        player.rb.isKinematic = true;
        player.transform.position = startPosition.position;
        cam.transform.position = startPosition.position;
    }

    private void Update()
    {
        player.transform.position = Vector3.SmoothDamp(player.transform.position, endPosition.position, ref velocity, timeToDescend);

        if (Vector3.Distance(player.transform.position,endPosition.transform.position) < 2f)
        {
            tileDecayManager.gameObject.SetActive(true);
            player.inCinematic = false;
            cam.isInCinematic = false;
            player.rb.isKinematic = false;
            AudioManager.Instance.PlayMusic("MainMusic");
            Destroy(gameObject);
        }
    }

}
