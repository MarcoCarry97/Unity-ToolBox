using System;
using UnityEngine;

namespace Band.Utils
{
    public class Rope : MonoBehaviour
    {
        [Range(1, 10)]
        [SerializeField]
        private float minLength;

        [Range(1, 10)]
        [SerializeField]
        private float maxLength;

        [Range(1, 10)]
        [SerializeField]
        private float elasticForce;

        [SerializeField]
        private Transform target;

        private Transform anchor;

        private Transform body;

        private void Start()
        {
            if (minLength > maxLength)
                minLength = maxLength;
            anchor = this.transform;
            body = this.transform.GetChild(0);
            DistanceConstraint();
        }

        private void Update()
        {
            DistanceConstraint();
            UpdateBodyPosition();
            UpdateBodyRotation();
            UpdateBodyScale();
        }

        private void UpdateBodyScale()
        {
            Vector3 scale = body.localScale;
            scale.y = Vector3.Distance(anchor.position, target.position);
            body.localScale = scale;
        }

        private void UpdateBodyRotation()
        {
            Vector3 axe = (target.position - anchor.position).normalized;
            body.rotation = Quaternion.FromToRotation(Vector3.up, axe);

        }

        private void UpdateBodyPosition()
        {
            body.position = (anchor.position + target.position) / 2;

        }

        private void DistanceConstraint()
        {
            Vector3 vector = target.position - anchor.position;
            print("Distance: " + vector.magnitude);
            float length = vector.magnitude;
            if (length > maxLength)
                length = maxLength;
            if (length < minLength)
                length = minLength;
            else length -= elasticForce * length * Time.deltaTime;
            //else length -= length * Time.fixedDeltaTime/elasticForce;
            //length -= minLength * length/maxLength *Time.deltaTime;
            target.position = anchor.position + vector.normalized * length;
            //float delta = length - maxLength;
            //target.position += 1 / 2 * delta * vector.normalized;
            //anchor.position -= 1 / 2 * delta * vector.normalized;
        }
    }
}