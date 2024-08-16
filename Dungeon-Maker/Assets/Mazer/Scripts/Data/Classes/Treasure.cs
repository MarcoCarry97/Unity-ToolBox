using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Treasure", menuName ="ScriptableObjects/Treasure")]
public class Treasure : ScriptableObject
{
    [SerializeField]
    private List<Item> content;

    public List<Item> Content { get { return content; } }


}
