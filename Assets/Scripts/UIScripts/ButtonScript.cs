using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonScript : MonoBehaviour
{
    public float duration;
    public float strenght;
    public int vibrato;
    public float randomness;
    

    
    void Start()
    {
        transform.DOShakePosition(duration, strenght, vibrato, randomness).SetLoops(250);
        
    }

    
}
