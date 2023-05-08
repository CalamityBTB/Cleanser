using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                ES3AutoSaveMgr.Current.Save();
                Debug.LogError("Saved");
            }
        }

    }


}
