using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraTP : MonoBehaviour
{
    public GameObject Player;
    public CinemachineConfiner2D confiner;
    public PolygonCollider2D Cave;
    public BoxCollider2D Wave1;
    public PolygonCollider2D Wave2;
    public BoxCollider2D Wave3;
    public BoxCollider2D Boss;
    private int roomNumber = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && roomNumber == 0)
        {
            roomNumber++;
            Player.transform.position = new Vector3(32f, -28f, 0f);
            confiner.m_BoundingShape2D = Wave1;
        }
        else if (other.CompareTag("Player") && roomNumber == 1)
        {
            roomNumber++;
            Player.transform.position = new Vector3(100f, -28f, 0f);
            confiner.m_BoundingShape2D = Wave2;
        }
        else if (other.CompareTag("Player") && roomNumber == 2)
        {
            roomNumber++;
            Player.transform.position = new Vector3(30f, -75f, 0f);
            confiner.m_BoundingShape2D = Wave3;
        }
        else if (other.CompareTag("Player") && roomNumber == 3)
        {
            roomNumber++;
            Player.transform.position = new Vector3(140f, -75f, 0f);
            confiner.m_BoundingShape2D = Boss;
        }
    }
}
