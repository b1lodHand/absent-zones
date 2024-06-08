using System;
using UnityEditor;
using UnityEngine;

namespace com.absence.zonesystem.editor
{
    public static class ZoneCreationHandler
    {
        [MenuItem("GameObject/absencee_/Zone/Empty Zone")]
        static void CreateZone_Default()
        {
            CreateZone_Internal(typeof(Zone));
        }

        [MenuItem("GameObject/absencee_/Zone/Unity Event Zone")]
        static void CreateZone_UnityEvent()
        {
            CreateZone_Internal(typeof(UnityEventZone));
        }

        [MenuItem("GameObject/absencee_/Zone/Audio Zone")]
        static void CreateZone_Audio()
        {
            CreateZone_Internal(typeof(AudioZone));
        }

        static void CreateZone_Internal(Type zoneType)
        {
            if (zoneType == null || !zoneType.BaseType.Equals(typeof(BaseZone)))
                throw new Exception($"Target type is not derived from '{nameof(BaseZone)}' type.");

            // create the zone game object.
            GameObject zoneObj = new GameObject($"New {zoneType.Name}");

            // set layer of the game object created if the Zone layer exists.
            int defaultZoneLayer = LayerMask.NameToLayer("Zone");
            if (defaultZoneLayer > -1) zoneObj.layer = LayerMask.NameToLayer("Zone");
            else Debug.LogWarning("No layers with name: 'Zone' in the project. Couldn't set the layer of newly created Zone.");

            // set position, rotation and the parent of the game object created.
            if (Selection.activeGameObject)
            {
                zoneObj.transform.SetParent(Selection.activeGameObject.transform);
                zoneObj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            Selection.activeGameObject = zoneObj;

            // undo
            Undo.RegisterCreatedObjectUndo(zoneObj, "New Zone Created (In Editor)");

            // lets user select the collider type the zone will use.
            bool colliderChoice = EditorUtility.DisplayDialog("Zone Collider", "What type of Collider will this Zone use?",
                "Box Collider", "Sphere Collider");

            // adds collider (needed by zone to function properly).
            Collider collider = colliderChoice ? zoneObj.AddComponent<BoxCollider>() : zoneObj.AddComponent<SphereCollider>();
            collider.isTrigger = true;

            // adds zone component.
            Component zoneComponent = zoneObj.AddComponent(zoneType);

            // moves zone component to the top.
            while (UnityEditorInternal.ComponentUtility.MoveComponentUp(zoneComponent)) ;

            // sends rename event after the creation.
            EditorApplication.delayCall += () =>
            {
                Type sceneHierarchyType = Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor");
                EditorWindow hierarchyWindow = EditorWindow.GetWindow(sceneHierarchyType);
                hierarchyWindow.SendEvent(EditorGUIUtility.CommandEvent("Rename"));
            };

        }
    }

}