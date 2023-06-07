using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    [SerializeField] private GameObject Obstacle;
    [SerializeField] private int _sortingOrder1 = -1;
    [SerializeField] private int _sortingOrder2 = 3;
    [SerializeField] private SpriteRenderer _spriteRendererObstacle;
    

    private void Awake()
    {
        _spriteRendererObstacle = Obstacle.GetComponent<SpriteRenderer>();
        
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           _spriteRendererObstacle.sortingOrder = _sortingOrder1;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spriteRendererObstacle.sortingOrder = _sortingOrder2;
        }
        
    }
}