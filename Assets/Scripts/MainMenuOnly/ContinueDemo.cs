using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ContinueDemo : MonoBehaviour
{

    public void Contin()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }
}
