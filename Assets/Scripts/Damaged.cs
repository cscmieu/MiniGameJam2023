using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Damaged : MonoBehaviour
{
    private Vector3Int _position;
    private TileData _data;
    private TileDecayManager _decayManager;
    private float _decayTimeCounter, _spreadIntervalCounter;
    
    public void StartDecayed(Vector3Int position, TileData data, TileDecayManager decayManager)
    {
        _position = position;
        _data = data;
        _decayManager = decayManager;

        _decayTimeCounter = data.decayTime;
        _spreadIntervalCounter = data.spreadInterval;
    }



    private void Update()
    {
        _decayTimeCounter -= Time.deltaTime;
        if(_decayTimeCounter <=0)
        {
            _decayManager.FinishedDecaying(_position);
            Destroy(gameObject);
        }

        _spreadIntervalCounter -= Time.deltaTime;
        if(_spreadIntervalCounter <=0)
        {
            _spreadIntervalCounter = _data.spreadInterval;
            _decayManager.TryToSpread(_position);
        }
        
    }
}
