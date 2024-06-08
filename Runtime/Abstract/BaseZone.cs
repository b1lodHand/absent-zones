using com.absence.zonesystem.imported;
using System;
using UnityEngine;

namespace com.absence.zonesystem
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseZone : MonoBehaviour
    {
        [System.Serializable]
        protected class ZoneGizmoData
        {
            protected static readonly float k_defaultGizmoColorAlpha = 0.6f;

            public bool constantGizmos = false;
            public Color gizmoColor = new(1f, 1f, 1f, k_defaultGizmoColorAlpha);
        }

        [SerializeField, Readonly] protected Collider m_collider;
        [SerializeField] protected ZoneGizmoData m_gizmoData = new();

        public event Action<GameObject> OnEnterEvent;
        public event Action<GameObject> OnExitEvent;

        protected void OnEnter(GameObject enteredOne)
        {
            OnEnter_Internal(enteredOne);
            OnEnterEvent?.Invoke(enteredOne);
        }

        protected void OnExit(GameObject exitedOne)
        {
            OnExit_Internal(exitedOne);
            OnExitEvent?.Invoke(exitedOne);
        }

        protected abstract void OnEnter_Internal(GameObject enteredOne);
        protected abstract void OnExit_Internal(GameObject exitedOne);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnEnter(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnExit(collision.gameObject);
        }

        #region Gizmos

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (!m_gizmoData.constantGizmos) return;

            DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (m_gizmoData.constantGizmos) return;

            DrawGizmos();
        }

#endif

        protected virtual void DrawGizmos()
        {
            Gizmos.color = m_gizmoData.gizmoColor;

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

        #endregion

        protected virtual void Reset()
        {
            m_collider = GetComponent<Collider>();
        }
    }

}