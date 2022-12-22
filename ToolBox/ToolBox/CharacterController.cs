using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace ToolBox.Control
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class CharacterController : MonoBehaviour
    {
        // Start is called before the first frame update

        public enum Mode
        {
            Velocity,
            MovePosition,
            AddForce
        }

        public Mode mode;
        public bool IsGrounded { get; private set; }

        private Vector3 lastDirection;

        private Rigidbody rigidBody;

        //private CharacterController characterController;

        private Vector3 currentPosition;

        private Vector3 newPosition;

        private Vector3 velocity;

        //private PixelPerfectCamera pixelPerfectCamera;

        private bool flipX = false;

        [Range(0, 10)]
        public float speed;

        [Range(0, 10)]
        public float acceleration;

        public void Move(Vector3 direction)
        {
            lastDirection = direction;
        }

        void Start()
        {

            lastDirection = Vector3.zero;
            velocity = Vector3.zero;
            rigidBody = this.GetComponent<Rigidbody>();
            //characterController=this.GetComponent<CharacterController>();
            currentPosition = newPosition = this.transform.position;

        }

        private void Update()
        {
            currentPosition = this.transform.position;
            velocity = computeVelocity(lastDirection);
            newPosition = currentPosition + velocity;
        }

        private Vector3 computeVelocity(Vector3 direction)
        {
            float speedTime = speed * Time.deltaTime;
            float accTime = acceleration * Time.deltaTime * Time.deltaTime;
            return direction * (speedTime + accTime);
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
        }

        private void UpdateRigidBody()
        {
            switch (mode)
            {
                case Mode.Velocity:
                    rigidBody.velocity = velocity;
                    break;
                case Mode.MovePosition:
                    rigidBody.MovePosition(newPosition);
                    break;
                case Mode.AddForce:
                    rigidBody.AddForce(velocity);
                    break;
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            IsGrounded = true;
        }

        void OnCollisionStay(Collision collision)
        {
            IsGrounded = true;
        }

        void OnCollisionExit(Collision collision)
        {
            IsGrounded = true;
        }


    }
}
