using UnityEngine;

namespace com.absence.zonesystem
{
    /// <summary>
    /// A zone type which only invokes C# actions when special events occur.
    /// </summary>
    [AddComponentMenu("absencee_/absent-zones/Built-in/Empty Zone")]
    public class EmptyZone : Zone
    {
        protected override void OnEnter_Internal(GameObject enteredOne)
        {

        }

        protected override void OnExit_Internal(GameObject exitedOne)
        {

        }
    }

}