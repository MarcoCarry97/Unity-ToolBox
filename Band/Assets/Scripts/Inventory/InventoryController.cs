using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    public InventoryItem Get(Inventory.Category cat, string itemName)
    {
        var value= inventory.Get(cat, itemName);
        return value.Item1;
    }

    public void Use(Inventory.Category cat, string itemName)
    {
        inventory.Use(cat, itemName);
    }

    public void Throw(Inventory.Category cat, string itemName)
    {
        inventory.Throw(cat, itemName);
    }
}
