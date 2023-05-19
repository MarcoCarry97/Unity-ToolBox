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
        GenerativePipeline generator = this.GetComponent<GenerativePipeline>();
        generator.Next = Input.GetButtonDown("Jump");
    }
}
