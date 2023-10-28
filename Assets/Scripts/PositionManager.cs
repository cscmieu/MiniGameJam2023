using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionManager : MonoBehaviour
{
    private Transform pos;
    private int id;
    [SerializeField] List<Transform> listPosition;
    [SerializeField] List<GameObject> objects; // objets à instantier

    void Start()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            id = Random.Range(0, listPosition.Count);
            pos = listPosition[id];
            Debug.Log(listPosition.Count);
            listPosition.Remove(listPosition[id]);
            Instantiate(objects[i], pos);
        }

    }

}
