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
    public float duration1;
    public float strenght1;
    public int vibrato1;
    public float randomness1;

    
    void Start()
    {
        transform.DOShakePosition(duration, strenght, vibrato, randomness).SetLoops(250);
        transform.DOShakeScale(duration1, strenght1, vibrato1, randomness1).SetLoops(250);
    }

    
}
