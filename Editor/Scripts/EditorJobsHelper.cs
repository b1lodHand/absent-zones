using System.Text;
using UnityEditor;
using UnityEngine;

namespace com.absence.zonesystem.editor
{
    public static class EditorJobsHelper
    {
        public static void PrintTypeCache()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<b>[ZONES] Types found in cache: </b>\n");

            sb.Append("<i>3D</i>");

            ZoneTypeCache.FoundZoneTypes3D.ForEach(zoneType =>
            {
                sb.Append("\n\t");

                sb.Append("<color=white>");
                sb.Append("-> ");
                sb.Append(zoneType.Name);
                sb.Append("</color>");
            });

            sb.Append("\n<i>2D</i>");

            ZoneTypeCache.FoundZoneTypes2D.ForEach(zoneType =>
            {
                sb.Append("\n\t");

                sb.Append("<color=white>");
                sb.Append("-> ");
                sb.Append(zoneType.Name);
                sb.Append("</color>");
            });

            Debug.Log(sb.ToString());
        }

        [MenuItem("absencee_/absent-zones/Refresh Type Cache")]
        static void Refresh_MenuItem()
        {
            ZoneTypeCache.Refresh(true);
        }
    }
}
