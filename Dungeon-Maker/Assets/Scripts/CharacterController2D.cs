using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
//[RequireComponent(typeof(PlayerInput))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private float speed;

    private Collider2D collider;

    private Rigidbody2D rigidBody;

    private PlayerInput playerInput;

    private Vector3 direction;

    private void Start()
    {
        playerInput = this.GetComponent<PlayerInput>();
        rigidBody= this.GetComponent<Rigidbody2D>();
        collider= this.GetComponent<Collider2D>();
    }

    public void Move(Vector3 direction)
    { 
        this.direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        Vector3 nextPosition = transform.position + direction * speed * Time.fixedDeltaTime;
        rigidBody.MovePosition(nextPosition);
        direction = Vector3.zero;
    }
}
