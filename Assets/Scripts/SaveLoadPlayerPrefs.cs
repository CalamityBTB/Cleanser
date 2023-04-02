using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadPlayerPrefs : MonoBehaviour
{
    
    public GameObject player;

    public void Save()
    {
       var x = player.transform.position.x;
       var y = player.transform.position.y;
        

        PlayerPrefs.SetFloat("x", x);
        PlayerPrefs.SetFloat("y", y);
        PlayerPrefs.Save();
        
    }

    //isminin böyle olmasının sebebi bu kolay olan sistem
    public void Loadd()
    {
       var x = PlayerPrefs.GetFloat("x");
       var y = PlayerPrefs.GetFloat("y");



        player.transform.position = new Vector2 (PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));
        SceneManager.LoadScene("MainScene");
        
    }

    public void onButtonSave()
    {
        Save();
    }

    public void onButtonLoad()
    {
        Loadd();
    }
    

}
