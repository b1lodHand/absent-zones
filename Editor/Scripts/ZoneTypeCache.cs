using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace com.absence.zonesystem.editor
{
    [InitializeOnLoad]
    public static class ZoneTypeCache
    {
        static List<Type> s_foundZoneTypes;

        public static int FoundTypeCount => s_foundZoneTypes.Count;
        public static List<Type> FoundZoneTypes => s_foundZoneTypes;
        public static bool NoZoneTypesFound => s_foundZoneTypes == null || s_foundZoneTypes.Count == 0;

        static ZoneTypeCache()
        {
            Refresh();
        }

        [MenuItem("absencee_/absent-zones/Refresh Type Cache")]
        static void Refresh_MenuItem()
        {
            Refresh();
        }

        public static void Refresh()
        {
            s_foundZoneTypes = TypeCache.GetTypesDerivedFrom<Zone>().ToList();
        }
    }
}
