using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Party", menuName = "ScriptableObjects/Party")]
public class Party : ScriptableObject, IEnumerable<MobInfo>
{
    [SerializeField]
    private List<MobInfo> currentParty;

    [SerializeField]
    private List<MobInfo> characters;

    public List<MobInfo> CurrentParty
    {
        get { return currentParty; }
    }

    public List<MobInfo> Everybody
    {
        get { return characters; }
    }

    public MobInfo this[int index]
    {
        get { return currentParty[index]; }
    }

    public void Add(MobInfo info)
    {
        characters.Add(info);
    }

    public MobInfo Remove(int index)
    {
        MobInfo info = characters[index];
        characters.RemoveAt(index);
        return info;
    }

    public void Swap(int index)
    {
        MobInfo tmp = currentParty[0];
        currentParty[0] = currentParty[index];
        currentParty[index] = tmp;
    }

    public void Swap(int a, int b)
    {
        MobInfo tmp = currentParty[a];
        currentParty[a] = characters[b];
        characters[b] = tmp;
    }

    public void Next()
    {
        MobInfo game=currentParty[0];
        currentParty.RemoveAt(0);
        currentParty.Add(game);
    }

    public IEnumerator<MobInfo> GetEnumerator()
    {
        return currentParty.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return currentParty.GetEnumerator();
    }
}
