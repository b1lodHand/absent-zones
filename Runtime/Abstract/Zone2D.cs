using com.absence.zonesystem.internals;
using UnityEngine;

namespace com.absence.zonesystem
{
    /// <summary>
    /// Base type to derive from if you want to create custom zone types (2D).
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public abstract class Zone2D : ZoneBase<Collider2D>
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnEnter(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnExit(collision.gameObject);
        }

        public override void DrawGizmos()
        {
            Gizmos.color = m_gizmoData.GizmoColor;

            var localScale = transform.localScale;
            var maxSize = Mathf.Max(localScale.x, localScale.y);

            if (m_collider is BoxCollider2D boxCollider)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawCube(boxCollider.offset, boxCollider.size);
            }

            else if (m_collider is CircleCollider2D sphereCollider)
            {
                Gizmos.DrawSphere(sphereCollider.offset,
                sphereCollider.radius * maxSize);
            }
        }

        protected override void Reset()
        {
            base.Reset();
        }
    }
}
