using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace ToolBox.Control.Platform2D
{
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float jumpHeigh;

        [SerializeField]
        private float jumpTime;

        [SerializeField]
        private float offsetBound;

        [SerializeField]
        private float maxJumps;

        private float jump;

        private float gravity;

        [SerializeField]
        private LayerMask layerMask;

        private Rigidbody2D rigidBody;

        private Collider2D collider;

        private Vector3 direction;

        private Vector3 velocity;

        private bool canJump;

        private bool continueJump;

        private int countJump;

        /*public bool IsGrounded
        {
            get
            {
                bool hit = DoBoxCast(Vector3.down, collider.bounds.extents.y);
                return hit;
            }
        }*/

        public bool IsGrounded
        {
            get
            {
                return rigidBody.Raycast(Vector3.down);
            }
        }

        public bool IsJumping { get; private set; }

        public bool IsFalling
        {
            get
            {
                return velocity.y <= 0 && !IsGrounded;
            }
        }


        private bool DoBoxCast(Vector3 direction, float distance)
        {
            RaycastHit hit;

            RaycastHit2D check = Physics2D.BoxCast(collider.bounds.center,
                collider.bounds.size,
                0,
                direction,
                distance + offsetBound,
                layerMask);
            return check.collider != null;
        }

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            jump = (2 * jumpHeigh) / (jumpTime / 2);
            gravity = (-2 * jumpHeigh) / Mathf.Pow(jumpTime / 2, 2);
            rigidBody.gravityScale = (gravity / 9.81f);
            countJump = 0;
        }

        private void Update()
        {
            if (IsGrounded)
                countJump = 0;
            ApplyMovement();
            ApplyJump();
            ApplyGravity();
            Prints();
        }

        private void FixedUpdate()
        {
            Vector3 newPosition = (Vector3)rigidBody.position + velocity * Time.fixedDeltaTime;
            rigidBody.MovePosition(newPosition);
        }

        public void Move(Vector3 direction)
        {
            this.direction = direction;
        }

        public void Jump(bool canJump, bool continueJump)
        {
            this.canJump = canJump;
            this.continueJump = continueJump;
        }

        private void ApplyMovement()
        {
            float moveSpeed = Mathf.MoveTowards(velocity.x,
                    direction.x * speed,
                    speed * Time.deltaTime);
            velocity.x = moveSpeed;
            if (velocity.x > 0f)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (velocity.x < 0f)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }

        private void ApplyJump()
        {
            if (IsGrounded && countJump == 0)
            {
                //velocity.y = Mathf.Max(velocity.y, 0);
                IsJumping = velocity.y >= 0;
                if (canJump)
                {
                    velocity.y = jump;
                    IsJumping = true;
                    countJump++;
                }

            }
            else if ((IsJumping || IsFalling) && countJump < maxJumps)
            {
                IsJumping = velocity.y > 0;
                if (canJump)
                {
                    velocity.y = jump;
                    IsJumping = true;
                    countJump++;
                }

            }
        }

        private void ApplyGravity()
        {
            float multiplier = velocity.y < 0 || !continueJump ? 2 : 1;
            velocity.y += multiplier * gravity * Time.deltaTime;
            velocity.y = Mathf.Max(velocity.y, gravity / 2);
        }

        private void Prints()
        {
            print("Ground: " + IsGrounded);
            print("Num. Jumps: " + countJump);
        }

        private void OnDrawGizmos()
        {
            bool hit = DoBoxCast(Vector3.down, 1);
            Gizmos.color = hit ? Color.green : Color.red;
            Vector3 center = collider.bounds.center + Vector3.down;
            Gizmos.DrawWireCube(center, collider.bounds.size);
        }
    }
}
