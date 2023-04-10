using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{

    public GameObject Inventory;
    public bool isActive;


    void Start()
    {
        Inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isActive)
            {
                DeactivateInventory();
            }

            else
            {
                ActivateInventory();
            }
        }
    }




    public void ActivateInventory()
    {
        Inventory.SetActive(true);
        isActive = true;
    }

    public void DeactivateInventory()
    {
        Inventory.SetActive(false);
        isActive = false;
    }
}
