using com.absence.zonesystem.internals;
using System;
using UnityEngine;

namespace com.absence.zonesystem
{
    /// <summary>
    /// Mark any zone types with enter/exit callbacks with this interface.
    /// </summary>
    public interface IZone
    {
        GameObject gameObject { get; }
        ZoneGizmoData GizmoData { get; }

        /// <summary>
        /// Action which will get invoked when any object enters the zone.
        /// </summary>
        event Action<GameObject> OnEnterEvent;
        /// <summary>
        /// Action which will get invoked when any object exits the zone.
        /// </summary>
        event Action<GameObject> OnExitEvent;

        /// <summary>
        /// The method will get called when this zone gets created in the editor.
        /// </summary>
        void OnCreation();
    }
}
