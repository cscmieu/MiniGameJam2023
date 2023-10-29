using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    private                  int              _id;
    [SerializeField] private List<Transform>  listPosition;
    [SerializeField] private List<GameObject> objects; // objets à instantier

    private void Start()
    {
        foreach (var t in objects)
            
        {
            _id = Random.Range(0, listPosition.Count);
            var pos = listPosition[_id];
            listPosition.Remove(listPosition[_id]);
            Instantiate(t, pos.position, Quaternion.identity);
        }
    }

}
