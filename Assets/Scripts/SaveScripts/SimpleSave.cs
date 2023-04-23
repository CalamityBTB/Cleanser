using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSave : MonoBehaviour
{
    public GameObject managerScript;

    
    

    public void Save()
    {
        ES3AutoSaveMgr.Current.Save();
       
    }
}
