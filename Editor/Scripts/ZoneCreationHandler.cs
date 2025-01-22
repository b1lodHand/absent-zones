using com.absence.zonesystem.builtin;
using System;
using UnityEditor;
using UnityEngine;

namespace com.absence.zonesystem.editor
{
    /// <summary>
    /// The static class responsible for creating zones via menu items.
    /// </summary>
    public static class ZoneCreationHandler
    {
        #region 3D Menu Elements

        [MenuItem("GameObject/absencee_/absent-zones/3D/Empty Zone", priority = 0)]
        static void CreateZone_Default()
        {
            CreateZone_Internal(typeof(EmptyZone));
        }

        [MenuItem("GameObject/absencee_/absent-zones/3D/Unity Event Zone", priority = 1)]
        static void CreateZone_UnityEvent()
        {
            CreateZone_Internal(typeof(UnityEventZone));
        }

        [MenuItem("GameObject/absencee_/absent-zones/3D/Audio Zone", priority = 2)]
        static void CreateZone_Audio()
        {
            CreateZone_Internal(typeof(AudioZone));
        }

        [MenuItem("GameObject/absencee_/absent-zones/3D/More Zone Types...", priority = 3)]
        static void InitiateZoneGrid()
        {
            ZoneSelectionWindow.Initiate(false);
        }

        #endregion

        #region 2D Menu Elements

        [MenuItem("GameObject/absencee_/absent-zones/2D/Empty Zone 2D", priority = 0)]
        static void CreateZone_Default2D()
        {
            CreateZone_Internal(typeof(EmptyZone2D));
        }

        [MenuItem("GameObject/absencee_/absent-zones/2D/Unity Event Zone 2D", priority = 1)]
        static void CreateZone_UnityEvent2D()
        {
            CreateZone_Internal(typeof(UnityEventZone2D));
        }

        [MenuItem("GameObject/absencee_/absent-zones/2D/Audio Zone 2D", priority = 2)]
        static void CreateZone_Audio2D()
        {
            CreateZone_Internal(typeof(AudioZone2D));
        }

        [MenuItem("GameObject/absencee_/absent-zones/2D/More Zone Types...", priority = 3)]
        static void InitiateZoneGridFor2D()
        {
            ZoneSelectionWindow.Initiate(true);
        }

        #endregion

        public static event Action<IZone> OnZoneCreation = delegate { };

        public static void CreateZone(Type zoneType)
        {
            CreateZone_Internal(zoneType);
        }
        static void CreateZone_Internal(Type zoneType)
        {
            bool is2D = zoneType.BaseType.Equals(typeof(Zone2D));
            bool is3D = zoneType.BaseType.Equals(typeof(Zone3D));

            if (zoneType == null || (!(is2D || is3D)))
                throw new Exception($"Target type is not valid for zone creation.");

            GameObject zoneObj = new GameObject($"New {zoneType.Name}");

            SetupZoneObject(zoneObj);

            Undo.RegisterCreatedObjectUndo(zoneObj, "New Zone Created (In Editor)");

            ColliderSelection(zoneObj, is2D);

            Component zoneComponent = zoneObj.AddComponent(zoneType);
            IZone zone = zoneObj.GetComponent<IZone>();

            OnZoneCreation?.Invoke(zone);
            zone.OnCreation();

            Cleanup(zoneComponent);
        }

        static void ColliderSelection(GameObject zoneObj, bool is2D)
        {
            if (is2D) ColliderSelection_2D(zoneObj);
            else ColliderSelection_3D(zoneObj);
        }
        static void ColliderSelection_2D(GameObject zoneObj)
        {
            int colliderChoice = EditorUtility.DisplayDialogComplex("Zone Collider", "What type of Collider will this Zone2D use?",
                "Box Collider 2D", "Circle Collider 2D", "Polygon Collider 2D");

            Collider2D collider;
            switch (colliderChoice)
            {
                case 0: // Box Collider 2D.
                    collider = zoneObj.AddComponent<BoxCollider2D>();
                    collider.isTrigger = true;
                    break;
                case 1: // Circle Collider 2D.
                    collider = zoneObj.AddComponent<CircleCollider2D>();
                    collider.isTrigger = true;
                    break;
                case 2: // Polygon Collider 2D.
                    collider = zoneObj.AddComponent<PolygonCollider2D>();
                    collider.isTrigger = true;
                    break;
            }
        }
        static void ColliderSelection_3D(GameObject zoneObj)
        {
            int colliderChoice = EditorUtility.DisplayDialogComplex("Zone Collider", "What type of Collider will this Zone use?",
                "Box Collider", "Sphere Collider", "Capsule Collider");

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
        }

        static void SetupZoneObject(GameObject zoneObj)
        {
            int defaultZoneLayer = LayerMask.NameToLayer("Zone");
            if (defaultZoneLayer > -1) zoneObj.layer = LayerMask.NameToLayer("Zone");
            else Debug.LogWarning("No layers with name: 'Zone' in the project. Couldn't set the layer of newly created Zone.");

            if (Selection.activeGameObject)
            {
                zoneObj.transform.SetParent(Selection.activeGameObject.transform);
                zoneObj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            Selection.activeGameObject = zoneObj;
        }
        static void Cleanup(Component zoneComponent)
        {
            while (UnityEditorInternal.ComponentUtility.MoveComponentUp(zoneComponent)) ;

            EditorApplication.delayCall += () =>
            {
                Type sceneHierarchyType = Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor");
                EditorWindow hierarchyWindow = EditorWindow.GetWindow(sceneHierarchyType);
                hierarchyWindow.SendEvent(EditorGUIUtility.CommandEvent("Rename"));
            };
        }
    }

}