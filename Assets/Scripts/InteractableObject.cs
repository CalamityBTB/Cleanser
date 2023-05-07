using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    private void OnInteract()
    {
        ES3AutoSaveMgr.Current.Save();
    }


}
