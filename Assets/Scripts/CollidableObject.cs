using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    private Collider2D I_collider;
    [SerializeField]
    private ContactFilter2D I_Filter;
    private List<Collider2D> I_CollidedObject = new List<Collider2D>(1);


    void Start()
    {
        I_collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        I_collider.OverlapCollider(I_Filter, I_CollidedObject);
       
    }
}
