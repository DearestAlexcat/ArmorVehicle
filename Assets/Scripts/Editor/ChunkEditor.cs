using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArmorVehicle
{
    [CustomEditor(typeof(Chunk))]
    public class ChunkEditor : Editor
    {
        private const float SphereRadius = 0.5f;
        private const int CircleSegments = 32;

        private Chunk chunk;

        public void OnEnable()
        {
            chunk = target as Chunk;
        }

        void OnSceneGUI()
        {
            if (chunk.environmentObjectPositions == null)
                return;

            for (int i = 0; i < chunk.environmentObjectPositions.Count; i++) 
            {
                EditorGUI.BeginChangeCheck();

                Vector3 worldPos = chunk.transform.TransformPoint(chunk.environmentObjectPositions[i]);
                Vector3 newWorldPos = Handles.PositionHandle(worldPos, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(chunk, "Move Spawn Point");
                    chunk.environmentObjectPositions[i] = chunk.transform.InverseTransformPoint(newWorldPos);
                    EditorUtility.SetDirty(chunk);
                }
            }

            for (int i = 0; i < chunk.environmentObjectPositions.Count; i++)
            {
                Vector3 worldPos = chunk.transform.TransformPoint(chunk.environmentObjectPositions[i]);

                Handles.color = Color.red;
                Handles.Label(worldPos + Vector3.up * 0.3f, $"#{i}");
                Handles.SphereHandleCap(0, worldPos, Quaternion.identity, 0.3f, EventType.Repaint);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginChangeCheck();

            GUILayout.BeginHorizontal();
            chunk.spawnZoneCenter = EditorGUILayout.Slider("Spawn Zone Center", chunk.spawnZoneCenter, 0.0f, 1.0f);
            GUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(chunk, "Modify Chunk Settings");
                EditorUtility.SetDirty(chunk);
            }
        }

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        static public void DrawCustomGizmo(Chunk chunk, GizmoType gizmoType)
        {
            Color color = Gizmos.color;

            Vector3 position = chunk.GetSpawnZonePosition();
            Gizmos.color = Color.green;

            Vector3 worldPos = position; // chunk.transform.TransformPoint(position);

            Gizmos.DrawSphere(worldPos, SphereRadius);
            DrawCircle(worldPos, chunk.spawnZoneRadius);

            Gizmos.color = color;
        }

        static void DrawCircle(Vector3 center, float radius)
        {
            float angleStep = 360f / CircleSegments;
            Vector3 prevPoint = center + new Vector3(Mathf.Cos(0f), 0f, Mathf.Sin(0f)) * radius;

            for (int i = 1; i <= CircleSegments; i++)
            {
                float angle = angleStep * i * Mathf.Deg2Rad;
                Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;

                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
        }
    }
}