using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClingoInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject game = GameObject.Find("Clingo");
        DungeonGenerator generator = this.GetComponent<DungeonGenerator>();
        generator.Next = Input.GetButtonDown("Jump");
    }
}
