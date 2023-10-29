using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    
    // Start is called before the first frame update
    private void Start()
    {
        var i = Random.Range(0, sprites.Count - 1);
        GetComponent<SpriteRenderer>().sprite = sprites[i];
    }

}
