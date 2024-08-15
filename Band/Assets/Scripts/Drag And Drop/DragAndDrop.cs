using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Band.DragAndDrop
{
    public class DragAndDrop : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 30)]
        private float rayLength;

        private float minDistance;

        private GameObject draggedObject;

        public bool Get()
        {
            Camera camera = Camera.main;
            Ray ray = new Ray(camera.transform.position, camera.transform.forward * rayLength);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            RaycastHit hit;
            bool check = Physics.Raycast(ray, out hit, rayLength);
            if (check && hit.collider.CompareTag("Piece") && hit.collider.gameObject.layer == LayerMask.NameToLayer("Piece"))
            {
                draggedObject = hit.collider.gameObject;
                return Hold();
            }
            else return false;
        }

        public bool Hold()
        {
            if (draggedObject != null)
            {
                draggedObject.GetComponent<Collider>().isTrigger = true;
                Vector3 distanceVector = draggedObject.transform.position - Camera.main.transform.position;
                float halfSize = draggedObject.transform.localScale.x + draggedObject.transform.localScale.y + draggedObject.transform.localScale.z;
                if (minDistance == 0)
                    minDistance = Mathf.Min(distanceVector.magnitude, rayLength);
                //minDistance = rayLength+this.transform.localScale.magnitude;
                Vector3 pos = this.transform.position + minDistance * Camera.main.transform.forward;
                draggedObject.transform.position = pos;
                Rigidbody rigidbody = draggedObject.GetComponent<Rigidbody>();
                rigidbody.isKinematic = true;
                return true;
            }
            else return false;
        }

        public bool Release()
        {
            draggedObject.GetComponent<Collider>().isTrigger = false;
            draggedObject = null;
            minDistance = 0;
            return true;

        }
    }
}
