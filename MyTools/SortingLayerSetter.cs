using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySortingLayerSetter : MonoBehaviour
{
    private SpriteRenderer rend;
    private int heightOffset;
    private int baseSortingOrder;

    // Start is called before the first frame update
    void Start()
    {
        rend = transform.GetComponent<SpriteRenderer>();
        heightOffset = 2;
        baseSortingOrder = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRendSorter(heightOffset, rend, baseSortingOrder);
    }

    //Changes the sprite sorting order of a gameObject based on its transform.y position, effectively making it appear behind objects closer to the camera 
    //and in front of objects when it is closer
    //offset is for determining the center of the gameObject if pivot is not sprite center
    public void SpriteRendSorter(int offset, SpriteRenderer spriteRend, int baseSortingOrder)
    {
        spriteRend.sortingOrder = (int)(baseSortingOrder - transform.position.y - offset);
    }
}
