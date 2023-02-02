using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace ToolBox.Control.Explorer2D
{


    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public partial class CharacterController2D : MonoBehaviour
    {

        [SerializeField]
        private LayerMask layerMask;

        

        private Vector3 direction;

        private Rigidbody2D rigidBody;

        new private Collider2D collider;

        private Vector3 velocity;

        [SerializeField]
        [Range(0, 10)]
        private float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public void Move(Vector3 direction)
        {
            this.direction = direction;
        }

        void Start()
        {
            direction = Vector3.zero;
            velocity = Vector3.zero;
            rigidBody = this.GetComponent<Rigidbody2D>();
            collider = this.GetComponent<Collider2D>();

        }

        private void Update()
        {
            velocity = computeVelocity(direction);
        }

        private Vector3 computeVelocity(Vector3 direction)
        {
            float speedTime = speed * Time.fixedDeltaTime;
            return ((speedTime) * direction);

        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
        }

        private void UpdateRigidBody()
        {
            Vector3 newPosition = (Vector3)rigidBody.position + velocity;
            rigidBody.MovePosition(newPosition);
        }

        private bool DoBoxCast(Vector3 direction, float distance)
        {
            RaycastHit hit;

            bool check = Physics.BoxCast(collider.bounds.center,
                    collider.bounds.extents / 2,
                    direction, out hit,
                    transform.rotation,
                    distance - 0.1f,
                    layerMask);
            return check;
        }

    }
}

