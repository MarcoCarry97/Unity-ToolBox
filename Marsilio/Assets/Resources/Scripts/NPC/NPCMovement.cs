using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ToolBox.Control.Explorer2D;
using UnityEngine;
using ToolBox.Animations;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(AnimatorController))]
public class NPCMovement : MonoBehaviour
{
    private Vector3 direction;

    private CharacterController2D controller;

    private AnimatorController animator;

    private Rigidbody2D rigidBody;

    [SerializeField]
    [Range(0,10)]
    private float stopTime;

    [SerializeField]
    [Range(0,10)]
    private float walkingTime;

    public enum State
    {
        Walking,
        Stop
    }

    private State state;

    private Coroutine moveCoroutine;

    private float timer;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        state=State.Stop;
        controller=this.GetComponent<CharacterController2D>();
        animator=this.GetComponent<AnimatorController>();
        timer = 0;
        rigidBody=this.GetComponent<Rigidbody2D>();
        //moveCoroutine=StartCoroutine(StopMovement());
        direction = ComputeDirection();
    }

    private void Update()
    {
        switch (state)
        {
            case (State.Walking):
                WalkingState();
                break;
            case (State.Stop):
                StopState();
                break;
        }

    }

    private void WalkingState()
    {
        if (timer < walkingTime)
        {
            timer += Time.deltaTime;
            DoMovement(direction);
        }
        else
        {
            timer = 0;
            state = State.Stop;
            animator.SetDirection(Vector3.zero);
        }
    }

    private void StopState()
    {
        if (timer < stopTime)
            timer += Time.deltaTime;
        else
        {
            timer = 0;
            state = State.Walking;
            direction = ComputeDirection();
        }
    }

    private void DoMovement(Vector3 direction)
    {
        controller.Move(direction);
        animator.SetDirection(direction);
    }

    private IEnumerator StopMovement()
    {
        yield return new WaitUntil(()=>timer >= walkingTime);
        state = State.Stop;
        direction = Vector3.zero;
        DoMovement(direction);
        yield return new WaitForSeconds(stopTime);
        direction = ComputeDirection();
        Vector3 currentPos=rigidBody.position;
        target = currentPos + (direction * controller.Speed);
        state =State.Walking;
        moveCoroutine = null;
    }

    private Vector3 ComputeDirection()
    {
        float horiz = Random.Range(-10, 10);
        float vert= Random.Range(-10, 10);
        horiz=Mathf.Clamp(horiz, -10, 10);
        vert=Mathf.Clamp(vert, -10, 10);
        Vector3 vector = new Vector3(horiz, vert, 0);
        return vector.normalized;
    }
}
