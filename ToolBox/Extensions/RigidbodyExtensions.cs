using UnityEngine;

namespace ToolBox.Extensions
{
    public static class RigidbodyExtensions
    {
        private static LayerMask layerMask = LayerMask.GetMask("Default");

        public static bool Circlecast(this Rigidbody2D rigidbody, Vector2 direction, float radius, float distance)
        {
            if (rigidbody.isKinematic)
            {
                return false;
            }

            RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
            return hit.collider != null && hit.rigidbody != rigidbody;
        }

        public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
        {
            Vector2 direction = other.position - transform.position;
            return Vector2.Dot(direction.normalized, testDirection) > 0;
        }

    }
}

