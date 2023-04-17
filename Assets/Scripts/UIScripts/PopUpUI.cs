using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour
{
    public GameObject PopUpPanel;

    public void ShowPopUp()
    {
        PopUpPanel.SetActive(true);
       
    }

    public void HidePopUp()
    {
        PopUpPanel.SetActive(false);
    }
    
}
