using UnityEngine;

namespace com.absence.zonesystem
{
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