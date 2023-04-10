using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    public GameObject Obstacle;
    public int SortingOrder = -1;
    public int SortingOrder2 = 3;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = Obstacle.GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           spriteRenderer.sortingOrder = SortingOrder;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sortingOrder = SortingOrder2;
        }

    }
}