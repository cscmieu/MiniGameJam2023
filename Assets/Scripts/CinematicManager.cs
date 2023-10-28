using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Transform startPosition;
    [SerializeField] private CameraMovement cam;
 
    void Start()
    {
        player._inputDisabled = true;
        cam.isInCinematic = true;
        player.transform.position = startPosition.position;
        cam.transform.position = startPosition.position;

    }


}
