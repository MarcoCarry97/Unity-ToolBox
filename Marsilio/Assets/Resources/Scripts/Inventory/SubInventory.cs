using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class SubInventory
{
    public Dictionary<string, (InventoryItem, uint)> pocket { get; private set; }

    public void Add(InventoryItem item)
    {
        if (pocket.ContainsKey(item.Name))
        {
            (InventoryItem, uint) value = pocket[item.Name];
            pocket[item.Name] = (value.Item1, value.Item2 + 1);
        }
        else pocket[item.Name] = (item, 1);
    }

    public (InventoryItem,uint) Get(string itemName)
    {
        (InventoryItem, uint) value=(null,0);
        if(pocket.ContainsKey(itemName))
            value= pocket[itemName];  
        return value;
    }

    public void Use(string itemName)
    {
        (InventoryItem, uint) value = (null, 0);
        if (pocket.ContainsKey(itemName))
        {
            value = pocket[itemName];
            if(value.Item2>1)
                pocket[itemName]= (value.Item1, value.Item2-1);
            else pocket.Remove(itemName);
        }
        if (value.Item1 != null) value.Item1.Use();
        else throw new InventoryException();
    }

    public void Throw(string itemName)
    {
        (InventoryItem, uint) value = (null, 0);
        if (pocket.ContainsKey(itemName))
        {
            value = pocket[itemName];
            if (value.Item2 > 1)
                pocket[itemName] = (value.Item1, value.Item2 - 1);
            else pocket.Remove(itemName);
        }
        if (value.Item1 == null)
            throw new InventoryException();
    }
}
