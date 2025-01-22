using UnityEngine;

namespace com.absence.zonesystem.builtin
{
    /// <summary>
    /// A zone type which only invokes C# actions when special events occur (2D).
    /// </summary>
    [AddComponentMenu("absencee_/absent-zones/Built-in/2D/Empty Zone")]
    public class EmptyZone2D : Zone2D
    {
        protected override void OnEnter_Internal(GameObject enteredOne)
        { 
        }

        protected override void OnExit_Internal(GameObject exitedOne)
        {
        }
    }
}
