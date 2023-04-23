using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleLoad : MonoBehaviour
{
    public GameObject managerScript;

    

    public void Load()
    {
        ES3AutoSaveMgr.Current.Load();
    }
    
}
