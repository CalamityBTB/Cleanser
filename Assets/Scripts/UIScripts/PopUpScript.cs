using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public PopUpUI popUpUI;

   

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            popUpUI.ShowPopUp();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        popUpUI.HidePopUp();
    }







}
