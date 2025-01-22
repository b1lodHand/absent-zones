using UnityEngine;

namespace com.absence.zonesystem.internals
{
    internal interface IZone2
    {
        void OnEnter(GameObject enteredOne);
        void OnExit(GameObject exitedOne);
        void DrawGizmos();
    }
}
