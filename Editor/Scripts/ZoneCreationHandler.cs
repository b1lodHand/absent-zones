using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace com.absence.zonesystem.editor
{
    public static class ZoneCreationHandler
    {

        [MenuItem("GameObject/absencee_/absent-zones/Empty Zone", priority = 0)]
        static void CreateZone_Default()
        {
            CreateZone_Internal(typeof(EmptyZone));
        }

        [MenuItem("GameObject/absencee_/absent-zones/Unity Event Zone", priority = 1)]
        static void CreateZone_UnityEvent()
        {
            CreateZone_Internal(typeof(UnityEventZone));
        }

        [MenuItem("GameObject/absencee_/absent-zones/Audio Zone", priority = 2)]
        static void CreateZone_Audio()
        {
            CreateZone_Internal(typeof(AudioZone));
        }

        [MenuItem("GameObject/absencee_/absent-zones/More Zone Types...", priority = 3)]
        static void InitiateZoneGrid()
        {
            ZoneSelectionWindow.Initiate();
        }

        public static void CreateZone(Type zoneType)
        {
            CreateZone_Internal(zoneType);
        }

        static void CreateZone_Internal(Type zoneType)
        {
            if (zoneType == null || !zoneType.BaseType.Equals(typeof(Zone)))
                throw new Exception($"Target type is not derived from '{nameof(Zone)}' type.");

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
            int colliderChoice = EditorUtility.DisplayDialogComplex("Zone Collider", "What type of Collider will this Zone use?",
                "Box Collider", "Sphere Collider", "Capsule Collider");

            // adds collider (needed by zone to function properly).

            Collider collider;
            switch (colliderChoice)
            {
                case 0: // Box Collider.
                    collider = zoneObj.AddComponent<BoxCollider>();
                    collider.isTrigger = true;
                    break;
                case 1: // Sphere Collider.
                    collider = zoneObj.AddComponent<SphereCollider>();
                    collider.isTrigger = true;
                    break;
                case 2: // Capsule Collider.
                    collider = zoneObj.AddComponent<CapsuleCollider>();
                    collider.isTrigger = true;
                    break;
            }

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