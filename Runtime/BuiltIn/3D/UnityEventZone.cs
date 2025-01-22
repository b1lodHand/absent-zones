using UnityEngine;
using UnityEngine.Events;

namespace com.absence.zonesystem.builtin
{
    /// <summary>
    /// A zone type which invokes UnityEvent based callbacks when special events occur (3D).
    /// </summary>
    [AddComponentMenu("absencee_/absent-zones/Built-in/3D/UnityEvent Zone")]
    public class UnityEventZone : Zone3D
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