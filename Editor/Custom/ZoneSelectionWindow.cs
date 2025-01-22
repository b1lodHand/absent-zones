using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.absence.zonesystem.editor
{
    /// <summary>
    /// The editor window that lets you select a zone type.
    /// </summary>
    public class ZoneSelectionWindow : EditorWindow
    {
        static bool s_targetFor2D = false;
        static int s_selectedZoneTypeIndex = 0;
        static GUIContent[] s_contents;

        public static void Initiate(bool targetFor2D)
        {
            s_targetFor2D = targetFor2D;

            ZoneTypeCache.Refresh(false);
            GenerateGridContent();
            ClampSelectionIndex();

            ZoneSelectionWindow window = (ZoneSelectionWindow)EditorWindow.GetWindow(typeof(ZoneSelectionWindow));
            window.titleContent = new GUIContent("More Zone Types...");
            window.Show();
        }

        private void OnGUI()
        {
            bool noTypes = s_targetFor2D ? 
                ZoneTypeCache.NoZoneTypesFound2D : ZoneTypeCache.NoZoneTypesFound3D;

            if (noTypes)
            {
                GUILayout.Label("There are no types derived from Zone in this project.");

                if (GUILayout.Button("Close"))
                {
                    Close();
                }

                return;
            }

            s_selectedZoneTypeIndex = GUILayout.SelectionGrid(s_selectedZoneTypeIndex, s_contents, 1);

            if (Event.current != null)
            {
                if (Event.current.isKey)
                {
                    switch (Event.current.keyCode)
                    {
                        case KeyCode.KeypadEnter:
                        case KeyCode.Return:
                            ApplySelection();
                            Close();
                            break;

                        case KeyCode.Escape:
                            Close();
                            break;

                        default:
                            break;
                    }
                }

                else if (Event.current.button == 0 && Event.current.clickCount == 2)
                {
                    ApplySelection();
                    Close();
                }
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Create"))
            {
                ApplySelection();
                Close();
            }

            if (GUILayout.Button("Cancel"))
            {
                Close();
            }
        }

        static void ApplySelection()
        {
            List<Type> foundTypes = null;
            if (s_targetFor2D) foundTypes = ZoneTypeCache.FoundZoneTypes2D;
            else foundTypes = ZoneTypeCache.FoundZoneTypes3D;

            ZoneCreationHandler.CreateZone(foundTypes[s_selectedZoneTypeIndex]);
        }

        static void ClampSelectionIndex()
        {
            int typeCount = ZoneTypeCache.FoundTypeCount;

            if (s_selectedZoneTypeIndex < 0) s_selectedZoneTypeIndex = 0;
            else if (s_selectedZoneTypeIndex >= typeCount) s_selectedZoneTypeIndex = 0;
        }

        static void GenerateGridContent()
        {
            List<Type> foundTypes = null;
            if (s_targetFor2D) foundTypes = ZoneTypeCache.FoundZoneTypes2D;
            else foundTypes = ZoneTypeCache.FoundZoneTypes3D;

            int typeCount = foundTypes.Count;

            s_contents = new GUIContent[typeCount];
            for (int i = 0; i < typeCount; i++)
            {
                Type type = foundTypes[i];

                s_contents[i] = new GUIContent()
                {
                    text = type.Name,
                    tooltip = type.FullName,
                    image = null,
                };
            }
        }
    }
}
