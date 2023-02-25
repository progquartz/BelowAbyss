using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{

    public void OnToggleInventory()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        }
    }

    public void OnOpeningLootingTable()
    {
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        }
    }
}


