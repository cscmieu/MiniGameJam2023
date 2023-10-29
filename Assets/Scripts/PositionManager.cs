using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionManager : MonoBehaviour
{
    private int id;
    [SerializeField] List<Transform> listPosition;
    [SerializeField] List<GameObject> objects; // objets � instantier

    void Start()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Transform pos;
            id = Random.Range(0, listPosition.Count);
            pos = listPosition[id];
            listPosition.Remove(listPosition[id]);
            var spawnedObj = Instantiate(objects[i], pos.position, Quaternion.identity);
            
        }

    }

}
