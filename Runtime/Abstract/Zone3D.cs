using com.absence.zonesystem.internals;
using UnityEngine;

namespace com.absence.zonesystem
{
    /// <summary>
    /// Base type to derive from if you want to create custom zone types (3D).
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class Zone3D : ZoneBase<Collider>
    {
        private void OnTriggerEnter(Collider collision)
        {
            OnEnter(collision.gameObject);
        }

        private void OnTriggerExit(Collider collision)
        {
            OnExit(collision.gameObject);
        }

        public override void DrawGizmos()
        {
            Gizmos.color = m_gizmoData.GizmoColor;

            var localScale = transform.localScale;
            var maxSize = Mathf.Max(localScale.x, localScale.y, localScale.z);

            if (m_collider is BoxCollider boxCollider)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawCube(boxCollider.center, boxCollider.size);
            }

            else if (m_collider is SphereCollider sphereCollider)
            {
                Gizmos.DrawSphere(sphereCollider.center + transform.position,
                sphereCollider.radius * maxSize);
            }
        }
    }
}