using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // Start is called before the first frame update
    enum State
    {
        Idle,
        Move
    }

    private State state;

    private Animator animator;

    private Vector3 lastDirection;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        state = State.Idle;
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Move:
                MoveState();
                break;
        }
    }

    private void IdleState()
    {
        if (lastDirection != Vector3.zero)
        {
            UpdateAnimator(lastDirection);
            lastDirection = Vector3.zero;
            state = State.Move;
        }
        else UpdateAnimator(lastDirection);
    }
    private void MoveState()
    {
        if (lastDirection != Vector3.zero)
        {
            spriteRenderer.flipX = lastDirection.x >= 0;
            UpdateAnimator(lastDirection);
        }
        else state = State.Idle;
    }
    private void UpdateAnimator(Vector3 direction)
    {
        if (animator)
        {
            (int, int) tuple = checkAnimation(direction);
            int x = tuple.Item1;
            int y = tuple.Item2;
            animator.SetInteger("WalkX", x);
            animator.SetInteger("WalkY", y);
        }

    }

    private (int, int) checkAnimation(Vector3 direction)
    {
        float cosineUp = Vector3.Dot(direction, Vector3.up);
        float cosineRight = Vector3.Dot(direction, Vector3.right);
        float cos45 = 0.7071068f;
        int x = 0, y = 0;
        if (cosineUp == 0 && cosineRight == 0)
        {
            x = 0;
            y = 0;
        }
        else if (cosineUp >= cos45 && cosineUp <= 1)
        {
            x = 0;
            y = -1;
        }
        else if (cosineUp <= -cos45 && cosineUp >= -1)
        {
            x = 0;
            y = 1;
        }
        else if (cosineRight < cos45 && cosineRight < -cos45)
        {
            x = 1;
            y = 0;
        }
        else //if (cosineRight > cos45 && cosineRight > -cos45)
        {
            x = -1;
            y = 0;
        }
        return (x, y);
    }

    public void SetDirection(Vector3 direction)
    {
        lastDirection = direction;
    }
}
