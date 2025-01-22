using com.absence.zonesystem.imported;
using System;
using UnityEngine;

namespace com.absence.zonesystem.internals
{
    /// <summary>
    /// This is the base type of all zones. However, I wouldn't recommend you to derive from this
    /// type because, the types you will create deriving from this type won't be shown
    /// in the zone type menus.
    /// </summary>
    /// <typeparam name="T">The component type derived zone type uses (eg. Collider, Collider2D).</typeparam>
    public abstract class ZoneBase<T> : MonoBehaviour, IZone, IZone2 where T : Component
    {
        [SerializeField] protected ZoneGizmoData m_gizmoData = new();
        [SerializeField, Readonly] protected T m_collider;

        public event Action<GameObject> OnEnterEvent = null;
        public event Action<GameObject> OnExitEvent = null;

        public ZoneGizmoData GizmoData => m_gizmoData;

        #region Public API

        public virtual void OnCreation()
        {
        }
        
        #endregion

        #region Internal API

        protected abstract void OnEnter_Internal(GameObject enteredOne); 
        protected abstract void OnExit_Internal(GameObject exitedOne);

        public void OnEnter(GameObject enteredOne)
        {
            OnEnter_Internal(enteredOne);
            OnEnterEvent?.Invoke(enteredOne);
        }
        public void OnExit(GameObject exitedOne)
        {
            OnExit_Internal(exitedOne);
            OnExitEvent?.Invoke(exitedOne);
        }

        public abstract void DrawGizmos();

        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!m_gizmoData.ConstantGizmos) return;

            DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (m_gizmoData.ConstantGizmos) return;

            DrawGizmos();
        }
#endif

        protected virtual void Reset()
        {
            m_collider = GetComponent<T>();
        }
    }
}
