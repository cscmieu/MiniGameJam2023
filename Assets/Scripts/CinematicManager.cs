using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform startPosition;
 
    void Start()
    {
        player.transform.position = startPosition.position;

    }


}
