using UnityEngine;
using UnityEngine.Events;

namespace com.absence.zonesystem
{
    public class UnityEventZone : Zone
    {
        [SerializeField] private UnityEvent m_onEnterUnityEvent;
        [SerializeField] private UnityEvent m_onExitUnityEvent;

        protected override void OnEnter_Internal(GameObject enteredOne)
        {
            m_onEnterUnityEvent?.Invoke();
        }

        protected override void OnExit_Internal(GameObject exitedOne)
        {
            m_onExitUnityEvent?.Invoke();
        }
    }

}