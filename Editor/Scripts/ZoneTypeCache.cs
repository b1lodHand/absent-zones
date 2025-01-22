using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace com.absence.zonesystem.editor
{
    /// <summary>
    /// The static class responsible for caching the zone types in the project.
    /// </summary>
    [InitializeOnLoad]
    public static class ZoneTypeCache
    {
        public const bool DEBUG_MODE = false;

        static List<Type> s_foundZoneTypes2D;
        static List<Type> s_foundZoneTypes3D;

        public static int FoundTypeCount => FoundTypeCount2D + FoundTypeCount3D;
        public static int FoundTypeCount2D => s_foundZoneTypes2D.Count;
        public static int FoundTypeCount3D => s_foundZoneTypes3D.Count;

        public static List<Type> FoundZoneTypes2D => s_foundZoneTypes2D;
        public static List<Type> FoundZoneTypes3D => s_foundZoneTypes3D;
        
        public static bool NoZoneTypesFound => NoZoneTypesFound2D && NoZoneTypesFound3D;
        public static bool NoZoneTypesFound2D => s_foundZoneTypes2D == null || s_foundZoneTypes2D.Count == 0;
        public static bool NoZoneTypesFound3D => s_foundZoneTypes3D == null || s_foundZoneTypes3D.Count == 0;

        static ZoneTypeCache()
        {
            Refresh(DEBUG_MODE);
        }

        /// <summary>
        /// Use to refresh the type cache.
        /// </summary>
        /// <param name="debugMode">If true, result will get displayed in the console window.</param>
        public static void Refresh(bool debugMode)
        {
            s_foundZoneTypes2D = TypeCache.GetTypesDerivedFrom<Zone2D>().ToList();
            s_foundZoneTypes3D = TypeCache.GetTypesDerivedFrom<Zone3D>().ToList();

            if (debugMode) EditorJobsHelper.PrintTypeCache();
        }
    }
}
