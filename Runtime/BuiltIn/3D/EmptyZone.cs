using UnityEngine;

namespace com.absence.zonesystem.builtin
{
    /// <summary>
    /// A zone type which only invokes C# actions when special events occur (3D).
    /// </summary>
    [AddComponentMenu("absencee_/absent-zones/Built-in/3D/Empty Zone")]
    public class EmptyZone : Zone3D
    {
        protected override void OnEnter_Internal(GameObject enteredOne)
        {

        }

        protected override void OnExit_Internal(GameObject exitedOne)
        {

        }
    }

}