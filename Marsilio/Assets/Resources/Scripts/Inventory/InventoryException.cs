using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryException : Exception
{
    public InventoryException(string message) : base(message)
    {

    }

    public InventoryException() : this("This item is not in the inventory!")
    {

    }
}
