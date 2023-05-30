using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeafScript : MonoBehaviour
{
    public Transform Leaf;
   
    void Start()
    {
        Leaf.DOMoveY(-750f, 10f).SetLoops(15);
        Leaf.DOMoveX(300f, 10f).SetLoops(15);
        Leaf.DORotate(new Vector3(0, 0, 50), 5f).SetLoops(15);
    }

    
}
