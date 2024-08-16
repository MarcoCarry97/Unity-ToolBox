using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Inventory",menuName ="ScriptableObjects/Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public enum Category
    {
        Important,
        Food,
        Battle,
        Equipment
    }

    private Dictionary<Category, SubInventory> bag;

    public Inventory()
    {
        bag= new Dictionary<Category,SubInventory>();

        bag[Category.Important] = new SubInventory();
        bag[Category.Food] = new SubInventory();
        bag[Category.Battle] = new SubInventory();
        bag[Category.Equipment] = new SubInventory();
    }

    public void Add(InventoryItem item)
    {
        bag[item.Category].Add(item);
    }

    public (InventoryItem,uint) Get(Category cat,string itemName)
    {
        return bag[cat].Get(itemName);
    }

    public void Use(Category cat, string name)
    {
        bag[cat].Use(name);
    }

    public void Throw(Category cat, string name)
    {
        bag[cat].Throw(name);
    }

}
