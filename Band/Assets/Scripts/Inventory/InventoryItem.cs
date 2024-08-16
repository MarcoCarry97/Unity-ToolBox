using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="InventoryItem",menuName ="ScriptableObjects/Inventory/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [SerializeField]
    private Inventory.Category category;

    public Inventory.Category Category { get { return category; } }

    [SerializeField]
    private string itemName;

    public string Name { get { return itemName; } }

    public void Use()
    {

    }
}
