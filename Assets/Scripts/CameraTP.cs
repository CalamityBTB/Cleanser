using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTP : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Camera.transform.position = new Vector3(20f, 0f, -10f);
            Player.transform.position = new Vector3(11.86f, -0.77f, 0f);
            
        }
    }
}
