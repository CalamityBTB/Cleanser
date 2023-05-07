using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    public GameObject Obstacle;
    [SerializeField] private int _sortingOrder1 = -1;
    [SerializeField] private int _sortingOrder2 = 3;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = Obstacle.GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           spriteRenderer.sortingOrder = _sortingOrder1;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.sortingOrder = _sortingOrder2;
        }

    }
}