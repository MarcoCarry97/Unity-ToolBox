using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        controller= this.GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        controller.Move(direction.normalized);
    }
}
