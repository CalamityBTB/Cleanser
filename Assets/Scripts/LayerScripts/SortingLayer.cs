using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    [SerializeField] private GameObject Obstacle;
    [SerializeField] private GameObject Roamer;
    [SerializeField] private int _sortingOrder1 = -1;
    [SerializeField] private int _sortingOrder2 = 3;
    [SerializeField] private SpriteRenderer _spriteRendererObstacle;
    [SerializeField] private SpriteRenderer _spriteRendererRoamer;

    private void Awake()
    {
        _spriteRendererObstacle = Obstacle.GetComponent<SpriteRenderer>();
        Roamer = GameObject.FindWithTag("Enemy");
        _spriteRendererRoamer = Roamer.GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           _spriteRendererObstacle.sortingOrder = _sortingOrder1;
        }
        if (other.CompareTag("Enemy"))
        {
            _spriteRendererRoamer.sortingOrder = _sortingOrder2;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spriteRendererObstacle.sortingOrder = _sortingOrder2;
        }
        if (other.CompareTag("Enemy"))
        {
            _spriteRendererRoamer.sortingOrder = _sortingOrder1;
        }
    }
}