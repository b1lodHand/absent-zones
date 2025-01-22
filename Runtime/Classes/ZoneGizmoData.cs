using UnityEngine;

namespace com.absence.zonesystem.internals
{
    /// <summary>
    /// The data type responsible for holding data about gizmos of the zones.
    /// </summary>
    [System.Serializable]
    public class ZoneGizmoData
    {
        public static readonly float DefaultColorAlpha = 0.6f;

        public bool ConstantGizmos = false;
        public Color GizmoColor = new(1f, 1f, 1f, DefaultColorAlpha);
    }
}
